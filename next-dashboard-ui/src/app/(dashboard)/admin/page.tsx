"use client";

import Announcements from "@/components/Announcements";
import AttendanceChart from "@/components/AttendanceChart";
import CountChart from "@/components/CountChart";
import EventCalendar from "@/components/EventCalendar";
import FinanceChart from "@/components/FinanceChart";
import UserCard from "@/components/UserCard";
import axios from "axios";
import { useEffect, useState } from "react";

const AdminPage = () => {
  const [studentCount, setStudentCount] = useState(0);
  const [boysCount, setBoysCount] = useState(0);
  const [girlsCount, setGirlsCount] = useState(0);

  useEffect(() => {
    const fetchStudents = async () => {
      try {
        const res = await axios.get("http://localhost:5160/api/students");
        const students = res.data;

        const boys = students.filter(
          (s: any) =>
            s.gender?.toLowerCase() === "male" || s.gender?.toLowerCase() === "nam"
        ).length;

        const girls = students.filter(
          (s: any) =>
            s.gender?.toLowerCase() === "female" || s.gender?.toLowerCase() === "nữ"
        ).length;

        setStudentCount(students.length);
        setBoysCount(boys);
        setGirlsCount(girls);

        console.log("Boys:", boys, "Girls:", girls);
      } catch (error) {
        console.error("Lỗi khi fetch students:", error);
      }
    };

    fetchStudents();
  }, []);

  return (
    <div className="p-4 flex gap-4 flex-col md:flex-row">
      {/* LEFT */}
      <div className="w-full lg:w-2/3 flex flex-col gap-8">
        {/* USER CARDS */}
        <div className="flex gap-4 justify-between flex-wrap">
          <UserCard type="student" count={studentCount} />
          <UserCard type="teacher" count={10} />
          <UserCard type="parent" count={10} />
          <UserCard type="staff" count={10} />
        </div>

        {/* MIDDLE CHARTS */}
        <div className="flex gap-4 flex-col lg:flex-row">
          <div className="w-full lg:w-1/3 h-[450px]">
            <CountChart boysCount={boysCount} girlsCount={girlsCount} />
          </div>
          <div className="w-full lg:w-2/3 h-[450px]">
            <AttendanceChart />
          </div>
        </div>

        {/* BOTTOM CHART */}
        <div className="w-full h-[500px]">
          <FinanceChart />
        </div>
      </div>

      {/* RIGHT */}
      <div className="w-full lg:w-1/3 flex flex-col gap-8">
        <EventCalendar />
        <Announcements />
      </div>
    </div>
  );
};

export default AdminPage;
