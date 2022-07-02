import httpClient from "@/axios/httpClient";

export default class BaseService<TEntity>
{
    private path: string;

    constructor(path: string) {
        this.path = path;
    }

    async GetAllAsync(): Promise<TEntity[]>{
        let result: Array<TEntity> = [];

        const response = await httpClient.get(`${this.path}`,
        {
            headers: {
                "Content-type": "application/json"
            }
        });

        return await response.data as TEntity[];
    }

    async GetByIdAsync(id: string): Promise<TEntity> {
        const response = await httpClient.get(`${this.path}/${id}`,
        {
            headers: {
                "Content-type": "application/json"
            }
        })

        return response.data as TEntity;
    }

    async AddAsync(entity: TEntity): Promise<number> {
        const result = await httpClient.post(`${this.path}`, entity,
            {
                headers: {
                    "Content-type": "application/json"
                }
            }
        )
        
        return result.status;
    }

    async RemoveAsync(id: string): Promise<number> {
        const result = await httpClient.delete(`${this.path}/${id}`,
        {
            headers: {
                "Content-type": "application/json"
            }
        });

        return result.status;
    }

    async UpdateAsync(id: string, entity: TEntity): Promise<number> {
        const result = await httpClient.put(`${this.path}/${id}`, entity,
        {
            headers: {
                "Content-type": "application/json"
            }
        });

        return result.status;
    }

}