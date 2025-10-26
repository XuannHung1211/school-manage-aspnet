"use client";

import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, Controller } from "react-hook-form";
import { z } from "zod";
import InputField from "../InputField";
import axios from "axios";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "../ui/select";
import { useEffect, useState } from "react";
import { toast } from "sonner";

const schema = z.object({
  studentName: z.string().min(1, { message: "Student name is required!" }),
  className: z.string().min(1, { message: "Class name is required!" }),
  birthday: z.coerce.date({ message: "Birthday is required!" }),
  gender: z.enum(["Male", "Female"], { message: "Gender is required!" }),
});

type Inputs = z.infer<typeof schema>;

const StudentForm = ({
  type,
  data,
  onSuccess,
}: {
  type: "create" | "update";
  data?: any;
  onSuccess?: (student:any)=> void;
}) => {
  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
    reset,
  } = useForm<Inputs>({
    resolver: zodResolver(schema),
  });

  const [classList, setClassList] = useState([]);

  useEffect(() => {
    const fetchClasses = async () => {
      try {
        const res = await axios.get("http://localhost:5160/api/classes");
        console.log("Response data:", res.data);
        setClassList(res.data);
      } catch (err) {
        console.error("Error fetching classes:", err);
      }
    };

    fetchClasses();
  }, []);

  const onSubmit = handleSubmit(async (data) => {
    try {
      if (type === "create") {
       const res = await axios.post("http://localhost:5160/api/students", {
          StudentName: data.studentName,
          ClassID: parseInt(data.className), // ID lớp
          BirthDate: data.birthday,
          Gender: data.gender,
        }); 
        toast.success("Create success")

        if (onSuccess) onSuccess(res.data);

        reset();
      }
    } catch (err) {
      console.error("Error creating student:", err);
      toast.error("Create failed")
    }
  });

  return (
    <form className="flex flex-col gap-8" onSubmit={onSubmit}>
      <h1 className="text-xl font-semibold">Create a new student</h1>
      <span className="text-xs text-gray-400 font-medium">
        Student Information
      </span>
      <div className="flex justify-between flex-wrap gap-4">
        <InputField
          label="StudentName"
          name="studentName"
          defaultValue={data?.studentName}
          register={register}
          error={errors?.studentName}
        />

        {/* 🔹 Select class có kết nối với form */}
        <Controller
          name="className"
          control={control}
          render={({ field }) => (
            <Select onValueChange={field.onChange} defaultValue={field.value}>
              <SelectTrigger className="w-[180px]">
                <SelectValue placeholder="Chọn lớp" />
              </SelectTrigger>
              <SelectContent>
                {classList && classList.length > 0 ? (
                  classList.map((cls: any, index: number) => (
                    <SelectItem key={cls.classID || index} value={String(cls.classID)}>
                      {cls.className}
                    </SelectItem>
                  ))
                ) : (
                  <SelectItem value="none" disabled>
                    Không có lớp nào
                  </SelectItem>
                )}
              </SelectContent>
            </Select>
          )}
        />
        {errors.className?.message && (
          <p className="text-xs text-red-400">
            {errors.className.message.toString()}
          </p>
        )}
      </div>

      <div className="flex justify-between flex-wrap gap-4">
        <InputField
          className="md:w-full"
          label="Birthday"
          name="birthday"
          defaultValue={data?.birthday}
          register={register}
          error={errors.birthday}
          type="date"
        />

        <div className="flex flex-col gap-2 w-full md:w-1/4">
          <label className="text-xs text-gray-500">Gender</label>
          <select
            className="ring-[1.5px] ring-gray-300 p-2 rounded-md text-sm w-full"
            {...register("gender")}
            defaultValue={data?.gender}
          >
            <option value="">Select gender</option>
            <option value="Male">Male</option>
            <option value="Female">Female</option>
          </select>
          {errors.gender?.message && (
            <p className="text-xs text-red-400">
              {errors.gender.message.toString()}
            </p>
          )}
        </div>
      </div>

      <button className="bg-blue-400 text-white p-2 rounded-md">
        {type === "create" ? "Create" : "Update"}
      </button>
    </form>
  );
};

export default StudentForm;
