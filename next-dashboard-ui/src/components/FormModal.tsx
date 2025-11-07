"use client";

import { Student } from "@/lib/student";
import dynamic from "next/dynamic";
import Image from "next/image";
import { useModalStore } from "@/store/state";

const TeacherForm = dynamic(() => import("./forms/TeacherForm"), {
  loading: () => <h1>Loading...</h1>,
});
const StudentForm = dynamic<{
  type: "create" | "update";
  data?: any;
  onSuccess?: (student: any) => void;
}>(() => import("./forms/StudentForm"), {
  loading: () => <h1>Loading...</h1>,
});

const forms: Record<
  string,
  (type: "create" | "update", data?: any, onSuccess?: (student: any) => void) => JSX.Element
> = {
  teacher: (type, data) => <TeacherForm type={type} data={data} />,
  student: (type, data, onSuccess) => (
    <StudentForm type={type} data={data} onSuccess={onSuccess} />
  ),
};

const FormModal = ({
  table,
  type,
  data,
  id,
  onConfirm,
  onSuccess,
}: {
  table:
    | "teacher"
    | "student"
    | "parent"
    | "subject"
    | "class"
    | "lesson"
    | "exam"
    | "assignment"
    | "result"
    | "attendance"
    | "event"
    | "announcement";
  type: "create" | "update" | "delete";
  data?: any;
  id?: number;
  onConfirm?: () => void;
  onSuccess?: (student: any) => void;
}) => {
  const { modalKey, openModal, closeModal } = useModalStore();
  const modalId = `${table}-${type}-${id ?? "new"}`; // 🔑 id riêng cho từng modal
  const isOpen = modalKey === modalId;

  const size = type === "create" ? "w-8 h-8" : "w-7 h-7";
  const bgColor =
    type === "create"
      ? "bg-lamaYellow"
      : type === "update"
        ? "bg-lamaSky"
        : "bg-lamaPurple";

  const handleDelete = async (e: React.FormEvent) => {
    e.preventDefault();
    if (onConfirm) await onConfirm();
    closeModal();
  };

  const Form = () => {
    if (type === "delete" && id) {
      return (
        <form onSubmit={handleDelete} className="p-4 flex flex-col gap-4">
          <span className="text-center font-medium">
            All data will be lost. Are you sure you want to delete this {table}?
          </span>
          <button
            type="submit"
            className="bg-red-700 text-white py-2 px-4 rounded-md border-none w-max self-center"
          >
            Delete
          </button>
        </form>
      );
    }

    if (type === "create" || type === "update") {
      return forms[table]
        ? forms[table](type, data, onSuccess)
        : <>Form not found!</>;
    }

    return <>Form not found!</>;
  };

  return (
    <>
      <button
        className={`${size} flex items-center justify-center rounded-full ${bgColor}`}
        onClick={() => openModal(modalId)} //  mở đúng modal
      >
        <Image src={`/${type}.png`} alt="" width={16} height={16} />
      </button>

      {isOpen && (
        <div className="w-screen h-screen absolute left-0 top-0 bg-black bg-opacity-60 z-50 flex items-center justify-center">
          <div className="bg-white p-4 rounded-md relative w-[90%] md:w-[70%] lg:w-[60%] xl:w-[50%] 2xl:w-[40%]">
            <Form />
            <div
              className="absolute top-4 right-4 cursor-pointer"
              onClick={() => closeModal()}
            >
              <Image src="/close.png" alt="" width={14} height={14} />
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export default FormModal;
