export default function fixStartTimeFormat(startTime: string) {
    const [date, time] = startTime.split("T");
    const [year, month, day] = date.split("-");
    const [hours, minutes] = time.split(":");

    return `${day}.${month}.${year} ${hours}:${minutes}`;
}