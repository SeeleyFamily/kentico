/*
 * Built with Common Launchpad 2.0.2
 */

const getSanitizeMediaUrl = (url) => {
    if (url.startsWith('~/'))
	{
		url = url.replace('~', '');
	}

	return url;
};

const getOptimizedImageUrl = (url, format) => {
	url = getSanitizeMediaUrl(url);
    if (url.toLowerCase().includes('/getmedia/'))
	{
		if (!url.includes('?'))
		{
			url = `${url}?`;
		}
		else
		{
			url = `${url}&`;
		}

        url = `/optimize${url}format=${format}`;
	}

	return url;
};

export default {
    getOptimizedImageUrl,
    getSanitizeMediaUrl
};