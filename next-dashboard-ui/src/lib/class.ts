import { Teacher } from "./teacher";
import { Student } from "./student";
import { Lesson } from "./lesson";
import { Exam } from "./exam";

export interface Class {
  classID: number;
  className: string;
  room?: string;

  teacherID?: number;
  teacher?: Teacher;

  students?: Student[];
  lessons?: Lesson[];
  exams?: Exam[];
}
