export interface UserTask {
    id: string;
    description: string;
    isDone: boolean;
    createdAt: Date;
    updatedAt: Date;
    userId: string;
}