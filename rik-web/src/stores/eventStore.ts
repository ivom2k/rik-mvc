import type IEvent from "@/domain/IEvent";
import EventService from "@/services/app/eventService";
import { defineStore } from "pinia";

const eventService = new EventService();

export const eventStore = defineStore("eventStore",
{
    state: () => ({
        events: [] as IEvent[]
    }),
    actions: {
        async fillEvents(): Promise<void> {
            this.events = await eventService.GetAllAsync();
        },
        async updateEvent(id: string, event: IEvent): Promise<void> {
            await eventService.UpdateAsync(id, event);
            await this.fillEvents();
        },
        async createEvent(event: IEvent): Promise<void> {
            await eventService.AddAsync(event);
        },
        async deleteEvent(id: string): Promise<void> {
            await eventService.RemoveAsync(id);
            await this.fillEvents();
        }
    }
})