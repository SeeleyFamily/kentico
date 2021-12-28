/*
 * Built with Common Launchpad 2.0.2
 */

export default {
	monthNames: ['Jan.', 'Feb.', 'Mar.', 'Apr.', 'May', 'June', 'July', 'Aug.', 'Sept.', 'Oct.', 'Nov.', 'Dec.'],
	isToday(someDate) {
        const today = new Date();
        return someDate.getDate() === today.getDate()
            && someDate.getMonth() === today.getMonth()
            && someDate.getFullYear() === today.getFullYear();
	},
	customFormatter(date, format) {
		let fDate = new Date(date);

		if (format === 'dash') {
			return `${(fDate.getMonth() + 1)}-${fDate.getDate()}-${fDate.getFullYear()}`;
		}
		else if (format === 'slash') {
            return `${(fDate.getMonth() + 1)}/${fDate.getDate()}/${fDate.getFullYear()}`;
		}
		else {
            return `${(fDate.getMonth() + 1)}/${fDate.getDate()}/${fDate.getFullYear()}`;
		}
	},
	formatDate(date) {
		let fDate = new Date(date);

		if (this.isToday(fDate)) {
			return `Today, ${this.monthNames[fDate.getMonth()]} ${fDate.getDate()}`;
		}
		else {
			return `${this.monthNames[fDate.getMonth()]} ${fDate.getDate()}`;
		}	
	},
	formatTime(date) {
		let fDate = new Date(date);
		let hours = fDate.getHours();
		let minutes = fDate.getMinutes();
		let ampm = hours >= 12 ? 'pm' : 'am';

		hours %= 12;
		hours = hours !== 0 ? hours : 12; // the hour '0' should be '12'
		minutes = minutes < 10 ? `0${minutes}` : minutes;

        let time = `${hours}:${minutes} ${ampm}`;

		return time;
	},
	formatHours(time) {
		let hours = Math.floor(time / 60);

		if (hours < 1) {
            return `${time}m`;
		}
		else {		
			let minutes = time % 60;

            return `${hours}h ${minutes}m`;
		}
	}
};