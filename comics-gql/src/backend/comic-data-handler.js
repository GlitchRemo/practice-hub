class ComicDataHandler {
	async getComic(id) {
		const res = await fetch(`https://xkcd.com/${id}/info.0.json`);
		return await res.json();
	}

	async getRandomComic() {
		const randomId = Math.ceil(Math.random() * 200);
		return await this.getComic(randomId);
	}
}

module.exports = { ComicDataHandler };
