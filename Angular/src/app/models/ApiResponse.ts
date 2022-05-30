export interface ApiResponse<T = void> {
    id: number;
    createdAt: Date;
    updatedAt: Date;
    data: T;
}
