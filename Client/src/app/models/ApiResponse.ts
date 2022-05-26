export interface ApiResponse<T> {
    id: number;
    createdAt: Date;
    updatedAt: Date;
    data: T;
}
