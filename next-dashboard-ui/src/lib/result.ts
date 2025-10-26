import { Student } from "./student";
import { Subject } from "./subject";

export interface Result {
  resultID: number;
  studentID: number;
  subjectID: number;
  score?: number;
  examDate?: string;

  student?: Student;
  subject?: Subject;
}
