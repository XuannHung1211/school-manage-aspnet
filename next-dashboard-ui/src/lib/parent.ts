import { Student } from "./student";

export interface Parent {
  parentID: number;
  parentName: string;
  email?: string;
  phone?: string;
  address?: string;

  studentID?: number;
  student?: Student;
}
