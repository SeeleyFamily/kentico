/*
 * Built with Common Launchpad 2.0.2
 */

function WysiwygConfig(toolset) {
    switch (toolset) {
        case 'min':
            return {
                buttons: 'bold,underline,italic',
                buttonsXS: 'bold,underline,italic',
                buttonsSM: 'bold,underline,italic',
                buttonsMD: 'bold,underline,italic'
            };
        case 'widget':
            return {
                buttons: 'bold,underline,italic,link,ul,ol'
            };
        default:
            return {};
    }
}

export default WysiwygConfig;