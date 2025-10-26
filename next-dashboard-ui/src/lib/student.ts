import { Class } from "./class";
import { Result } from "./result";
import { Parent } from "./parent";

export interface Student {
  studentID: number;
  studentName: string;
  birthDate?: string;   // ISO string (YYYY-MM-DD)
  gender?: string;

  classID?: number;
  class?: Class;

  results?: Result[];
  parents?: Parent[];
}
