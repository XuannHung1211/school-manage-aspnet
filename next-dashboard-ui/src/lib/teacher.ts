import { Class } from "./class";
import { Lesson } from "./lesson";
import { Exam } from "./exam";

export interface Teacher {
  teacherID: number;
  teacherName: string;
  email?: string;
  phone?: string;

  classes?: Class[];
  lessons?: Lesson[];
  exams?: Exam[];
}
