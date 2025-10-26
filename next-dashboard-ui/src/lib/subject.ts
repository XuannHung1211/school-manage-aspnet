import { Result } from "./result";
import { Lesson } from "./lesson";
import { Exam } from "./exam";

export interface Subject {
  subjectID: number;
  subjectName: string;
  description?: string;

  results?: Result[];
  lessons?: Lesson[];
  exams?: Exam[];
}
