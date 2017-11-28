export class Notification
{
    public message: string;
    public statusCode: number;

    public constructor(message: string, statusCode: number)
    {
        this.message = message;
        this.statusCode = statusCode;
    }
}