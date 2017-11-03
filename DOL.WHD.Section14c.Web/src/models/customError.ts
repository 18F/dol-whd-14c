
export class customError extends Error {
  message: string;
  level: string;
  ein: string;
  exception: string;
  userId: string;
  user: string;

  constructor(message:string, level: string, ein?: string, userId?: string, user?: string, exception?: string) {
    super(message);
    this.message = message;
    this.ein = ein;
    this.userId = userId;
    this.user = user;
    this.level = level;
    this.exception = exception;
  }
}
