import { Subject } from "./subject";
import { Teacher } from "./teacher";
import { Class } from "./class";

export interface Lesson {
  lessonID: number;
  lessonName?: string;
  lessonDate?: string;

  subjectID?: number;
  subject?: Subject;

  teacherID?: number;
  teacher?: Teacher;

  classID?: number;
  class?: Class;
}
