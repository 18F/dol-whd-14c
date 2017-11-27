
export class customError extends Error {
  Message: string;
  Level: string;
  EIN: string;
  Exception: string;
  UserId: string;
  User: string;

  constructor(Message:string, Level: string, EIN?: string, UserId?: string, User?: string, Exception?: string) {
    super(Message);
    this.Message = Message;
    this.EIN = EIN;
    this.UserId = UserId;
    this.User = User;
    this.Level = Level;
    this.Exception = Exception;
  }
}
