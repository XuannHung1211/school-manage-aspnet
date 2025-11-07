"use client";

import FormModal from "@/components/FormModal";
import Pagination from "@/components/Pagination";
import Table from "@/components/Table";
import TableSearch from "@/components/TableSearch";
import { role } from "@/lib/data";
import { Student } from "@/lib/student";
import { useModalStore } from "@/store/state";
import axios from "axios";
import { format } from "date-fns";
import Image from "next/image";
import Link from "next/link";
import { useEffect, useState } from "react";
import { toast } from "sonner";
import {
  Avatar,
  AvatarFallback,
  AvatarImage,
} from "@/components/ui/avatar"


const columns = [
  { header: "Info", accessor: "info" },
  { header: "BirthDay", accessor: "studentId", className: "hidden md:table-cell" },
  { header: "Class", accessor: "grade", className: "hidden md:table-cell" },
  { header: "Gender", accessor: "phone", className: "hidden md:table-cell" },
  { header: "Actions", accessor: "action" },
];

const StudentListPage = () => {
  const [students, setStudents] = useState<Student[]>([]);
  const [currentPage, setCurrentPage] = useState<number>(1);
  const [pageSize] = useState<number>(8); // số sinh viên mỗi trang
  const [totalPages, setTotalPages] = useState<number>(0);

  const {closeModal} = useModalStore()

  //  Hàm fetch sinh viên có phân trang
  const fetchStudents = async () => {
    try {
      const res = await axios.get("http://localhost:5160/api/students", {
        params: {
          pageNumber: currentPage, // backend nhận pageNumber, không phải currentPage
          pageSize: pageSize,
        },
      });

      setStudents(res.data.data || res.data.Data); // tương thích cả 2 cách viết (Data / data)
      setTotalPages(res.data.totalPages || res.data.TotalPages);

      console.log("Dữ liệu từ API:", res.data);
    } catch (error) {
      console.error(" Lỗi khi fetch student:", error);
    }
  };

  useEffect(() => {
    fetchStudents();
  }, [currentPage, pageSize]);

  //  Nếu xóa hết sinh viên ở trang cuối → quay lại trang trước
  useEffect(() => {
    if (currentPage > totalPages && totalPages > 0) {
      setCurrentPage(totalPages);
    }
  }, [students, totalPages, currentPage]);

  // 🗑 Xóa sinh viên
  const handleDelete = async (id: number) => {
    try {
      await axios.delete(`http://localhost:5160/api/students/${id}`)
      toast.dismiss()
      toast.success("Deleted success");
      await fetchStudents(); // load lại danh sách sau khi xóa
    } catch (error) {
      toast.dismiss()
      toast.error("Deleted fail");
      console.error("Check: ", error);
    }
  };

  //  Thêm sinh viên
  const handleCreate = async (newStudent: Student) => {
    toast.dismiss()
    toast.success("Added student success!");
    await fetchStudents(); 
    closeModal()
    
  };

  // 🧩 Render từng dòng trong bảng
  const renderRow = (item: Student) => (
    <tr
      key={item.studentID}
      className="border-b border-gray-200 even:bg-slate-50 text-sm hover:bg-lamaPurpleLight"
    >
      <td className="flex items-center gap-4 p-4">
       
            <Avatar className="md:hidden xl:block ">
            <AvatarImage loading="lazy" src={`https://i.pravatar.cc/150?${Math.floor(Math.random() * 20) + 1}`} alt="https://i.pravatar.cc/name=XuanHung" />
              <AvatarFallback>XH</AvatarFallback>
            </Avatar>

        <div className="flex flex-col">
          <h3 className="font-semibold">{item.studentID}</h3>
          <p className="text-xs text-gray-500">{item.studentName}</p>
        </div>
      </td>
      <td className="hidden md:table-cell">
        {item?.birthDate ? format(new Date(item.birthDate), "dd-MM-yyyy") : "—"}
      </td>
      <td className="hidden md:table-cell">{item.class?.className || "—"}</td>
      <td className="hidden md:table-cell">{item.gender || "—"}</td>

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
      <Table columns={columns} renderRow={renderRow} data={students} />

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
