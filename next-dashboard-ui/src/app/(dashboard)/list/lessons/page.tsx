"use client"
import FormModal from "@/components/FormModal";
import Pagination from "@/components/Pagination";
import Table from "@/components/Table";
import TableSearch from "@/components/TableSearch";
import { Lesson } from "@/lib/lesson";
import axios from "axios";
import Image from "next/image";
import { useEffect, useState } from "react";



const columns = [
  {
    header: "Subject Name",
    accessor: "name",
  },
  {
    header: "Class",
    accessor: "class",
  },
  {
    header: "Teacher",
    accessor: "teacher",
    className: "hidden md:table-cell",
  },
  {
    header: "Actions",
    accessor: "action",
  },
];

const LessonListPage = () => {

  const [lessons , setLessons] = useState<Lesson[]>([])

  useEffect(() => {
    const fetchLesson = async () => {
      try {
        const res = await axios.get("http://localhost:5160/api/lessons")
        console.log(res.data)
        setLessons(res.data)
      } catch (error) {
        console.log("Lỗi fetch Lesson" , error)
      }
    }

    fetchLesson()

  } , [])
  const renderRow = (item: Lesson) => (
    <tr
      key={item.lessonID}
      className="border-b border-gray-200 even:bg-slate-50 text-sm hover:bg-lamaPurpleLight"
    >
      <td className="flex items-center gap-4 p-4">{item.subject?.subjectName}</td>
      <td>{item.class?.className}</td>
      <td className="hidden md:table-cell">{item.teacher?.teacherName}</td>
      <td>
        <div className="flex items-center gap-2">   
              <FormModal table="lesson" type="update" data={item} />
              <FormModal table="lesson" type="delete" id={item.lessonID} />
        </div>
      </td>
    </tr>
  );

  return (
    <div className="bg-white p-4 rounded-md flex-1 m-4 mt-0">
      {/* TOP */}
      <div className="flex items-center justify-between">
        <h1 className="hidden md:block text-lg font-semibold">All Lessons</h1>
        <div className="flex flex-col md:flex-row items-center gap-4 w-full md:w-auto">
          <TableSearch />
          <div className="flex items-center gap-4 self-end">
            <button className="w-8 h-8 flex items-center justify-center rounded-full bg-lamaYellow">
              <Image src="/filter.png" alt="" width={14} height={14} />
            </button>
            <button className="w-8 h-8 flex items-center justify-center rounded-full bg-lamaYellow">
              <Image src="/sort.png" alt="" width={14} height={14} />
            </button>

          </div>
        </div>
      </div>
      {/* LIST */}
      <Table columns={columns} renderRow={renderRow} data={lessons} />
      {/* PAGINATION */}
      <Pagination 
        currentPage={1}
        totalPages={1}
        onPageChange={(page) => page}
      />
    </div>
  );
};

export default LessonListPage;
