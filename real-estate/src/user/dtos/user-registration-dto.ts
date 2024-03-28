import { Role } from "../enums/role";

export class UserRegistrationDto {
  username: string;
  password: string;
  role: Role;
}
