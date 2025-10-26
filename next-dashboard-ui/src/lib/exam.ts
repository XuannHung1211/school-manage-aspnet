import { Subject } from "./subject";
import { Class } from "./class";
import { Teacher } from "./teacher";

export interface Exam {
  examID: number;
  examName?: string;
  examDate?: string;

  subjectID?: number;
  subject?: Subject;

  classID?: number;
  class?: Class;

  teacherID?: number;
  teacher?: Teacher;
}
