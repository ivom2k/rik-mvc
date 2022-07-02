import type IParticipation from "@/domain/IParticipation";
import BaseService from "../base/baseService";

export default class ParticipationService extends BaseService<IParticipation> {
    constructor() {
        super("participations");
    }
}