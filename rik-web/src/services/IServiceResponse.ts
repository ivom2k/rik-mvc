export default interface IServiceResponse<TData> {
    status: number;
    data?: TData;
    errorMessage?: string;
}