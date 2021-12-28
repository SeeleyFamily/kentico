/*
 * Built with Common Launchpad 2.0.2
 */

import * as CONST from './constants';

// utility class for reusable ui related functions

export default class Util {
	// determine if the current device width is desktop, tablet or mobile
	// if (Util.getViewPort() === CONST.ViewPort.Desktop) { // do something if the viewport is desktop })
	static getViewPort() {
		const desktopBreakpoint = CONST.Breakpoints.Desktop;
		const tabletBreakpoint = CONST.Breakpoints.Tablet;

		const width = window.innerWidth;

		if (width >= desktopBreakpoint) {
			return CONST.ViewPort.Desktop;
		}
		else if (width >= tabletBreakpoint) {
			return CONST.ViewPort.Tablet;
		}
		else {
			return CONST.ViewPort.Mobile;
		}
	}
}