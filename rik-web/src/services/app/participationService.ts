import httpClient from "@/axios/httpClient";
import type IParticipation from "@/domain/IParticipation";
import BaseService from "../base/baseService";

export default class ParticipationService extends BaseService<IParticipation> {
    constructor() {
        super("participations");
    }

    async GetAllWithEventAsync(idEvent: string): Promise<IParticipation[]>{
        let result: Array<IParticipation> = [];

        const response = await httpClient.get(`companies?idevent=${idEvent}`,
        {
            headers: {
                "Content-type": "application/json"
            }
        });

        return await response.data as IParticipation[];
    }
}