import type IEvent from "@/domain/IEvent";
import BaseService from "../base/baseService";

export default class EventService extends BaseService<IEvent> {
    constructor() {
        super("events");
    }
}