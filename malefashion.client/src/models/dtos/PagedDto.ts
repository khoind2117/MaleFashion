export interface PagedDto<T> {
    totalRecords: number;
    pageSize: number;
    pageNumber: number;
    items: T[];
    hasNextPage: boolean;
    hasPreviousPage: boolean;
}