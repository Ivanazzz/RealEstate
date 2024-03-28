import { Role } from "../enums/role";

export class UserDto {
  id: number;
  username: string;
  role: Role;
}
