/*
 * Built with Common Launchpad 2.0.2
 */

class Analytics {
    gtmClickEvent(eventName, attributeList) {
        window.dataLayer = window.dataLayer || [];
        window.dataLayer.push({
            event: eventName,
            attributes: attributeList
        });
    }
}

export default Analytics;