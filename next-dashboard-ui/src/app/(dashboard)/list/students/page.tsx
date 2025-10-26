"use client";

import FormModal from "@/components/FormModal";
import Pagination from "@/components/Pagination";
import Table from "@/components/Table";
import TableSearch from "@/components/TableSearch";
import { role } from "@/lib/data";
import { Student } from "@/lib/student";
import axios from "axios";
import { format } from "date-fns";
import Image from "next/image";
import Link from "next/link";
import { useEffect, useState } from "react";
import { toast } from "sonner";

const columns = [
  { header: "Info", accessor: "info" },
  { header: "BirthDay", accessor: "studentId", className: "hidden md:table-cell" },
  { header: "Class", accessor: "grade", className: "hidden md:table-cell" },
  { header: "Gender", accessor: "phone", className: "hidden md:table-cell" },
  { header: "Score", accessor: "address", className: "hidden md:table-cell" },
  { header: "Actions", accessor: "action" },
];

const StudentListPage = () => {
  const [students, setStudents] = useState<Student[]>([]);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [pageSize] = useState<number>(8); // số sinh viên mỗi trang

  useEffect(() => {
    const fetchStudent = async () => {
      try {
        const res = await axios.get("http://localhost:5160/api/students");
        setStudents(res.data);
        console.log("Dữ liệu từ API:", res.data);
      } catch (error) {
        console.error("Lỗi khi fetch student:", error);
      }
    };

    fetchStudent();
  }, []);

  // Tính tổng số trang
  const totalPages = Math.ceil(students.length / pageSize);

  // Cắt dữ liệu hiển thị theo trang hiện tại
  const paginatedStudents = students.slice(
    (currentPage - 1) * pageSize,
    currentPage * pageSize
  );

  const handleDelete = async (id: number) => {
    try {
      await axios.delete(`http://localhost:5160/api/students/${id}`);
      toast.success("Delete success");
      setStudents((prev) => prev.filter((s) => s.studentID !== id));
    } catch (error) {
      toast.error("Delete Error");
      console.log("Check: ", error);
    }
  };

  const handleCreate = (newStudent: Student) => {
    setStudents((prev) => [...prev, newStudent]);
    toast.success("Student added successfully!");
  };

  const renderRow = (item: Student) => (
    <tr
      key={item.studentID}
      className="border-b border-gray-200 even:bg-slate-50 text-sm hover:bg-lamaPurpleLight"
    >
      <td className="flex items-center gap-4 p-4">
        <div className="md:hidden xl:block w-10 h-10 rounded-full object-cover bg-blue-100 text-xl flex items-center justify-center text-center font-bold">
          <span className="text-slate-400 ">
            {item.studentName[0].toUpperCase()}
          </span>
        </div>
        <div className="flex flex-col">
          <h3 className="font-semibold">{item.studentID}</h3>
          <p className="text-xs text-gray-500">{item.studentName}</p>
        </div>
      </td>
      <td className="hidden md:table-cell">
        {item?.birthDate && format(item?.birthDate, "dd-MM-yyyy")}
      </td>
      <td className="hidden md:table-cell">{item.class?.className}</td>
      <td className="hidden md:table-cell">{item.gender}</td>
      <td className="hidden md:table-cell">
        {item.results?.map((r) => r.score).join(", ")}
      </td>
      <td>
        <div className="flex items-center gap-2">
          <Link href={`/list/students/${item.studentID}`}>
            <button className="w-7 h-7 flex items-center justify-center rounded-full bg-lamaSky">
              <Image src="/view.png" alt="" width={16} height={16} />
            </button>
          </Link>
          {role === "admin" && (
            <FormModal
              table="student"
              type="delete"
              id={item.studentID}
              onConfirm={() => handleDelete(item.studentID)}
              onSuccess={(newStudent) => {
                setStudents((prev) => [...prev, newStudent]);
              }}
            />
          )}
        </div>
      </td>
    </tr>
  );

  return (
    <div className="bg-white p-4 rounded-md flex-1 m-4 mt-0">
      {/* TOP */}
      <div className="flex items-center justify-between">
        <h1 className="hidden md:block text-lg font-semibold">All Students</h1>
        <div className="flex flex-col md:flex-row items-center gap-4 w-full md:w-auto">
          <TableSearch />
          <div className="flex items-center gap-4 self-end">
            <button className="w-8 h-8 flex items-center justify-center rounded-full bg-lamaYellow">
              <Image src="/filter.png" alt="" width={14} height={14} />
            </button>
            <button className="w-8 h-8 flex items-center justify-center rounded-full bg-lamaYellow">
              <Image src="/sort.png" alt="" width={14} height={14} />
            </button>
            {role === "admin" && (
              <FormModal table="student" type="create" onSuccess={handleCreate} />
            )}
          </div>
        </div>
      </div>

      {/* LIST */}
      <Table columns={columns} renderRow={renderRow} data={paginatedStudents} />

      {/* PAGINATION */}
      <Pagination
        currentPage={currentPage}
        totalPages={totalPages}
        onPageChange={(page) => setCurrentPage(page)}
      />
    </div>
  );
};

export default StudentListPage;
