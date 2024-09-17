export interface ApiResponse<T> {
    data?: T;
    totalItems?: number;
    errors?: string[];
}