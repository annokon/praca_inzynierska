export function getProfileImageFromResponse(data) {
    if (!data) return "";

    if (typeof data === "string") {
        return data;
    }

    if (data.profile) return data.profile;
    if (data.profileImage) return data.profileImage;
    if (data.profileImageUrl) return data.profileImageUrl;

    if (data.image) return data.image;
    if (data.imageUrl) return data.imageUrl;
    if (data.path) return data.path;
    if (data.filePath) return data.filePath;

    if (Array.isArray(data.images)) {
        const profileImage =
            data.images.find((image) => image.isProfile) ||
            data.images.find((image) => image.isMain) ||
            data.images[0];

        return getProfileImageFromResponse(profileImage);
    }

    if (Array.isArray(data.userImages)) {
        const profileImage =
            data.userImages.find((image) => image.isProfile) ||
            data.userImages.find((image) => image.isMain) ||
            data.userImages[0];

        return getProfileImageFromResponse(profileImage);
    }

    return "";
}

export function getImageSrc(imagePath) {
    if (!imagePath) return "";

    if (imagePath.startsWith("http://") || imagePath.startsWith("https://")) {
        return encodeURI(imagePath);
    }

    if (imagePath.startsWith("/")) {
        return encodeURI(`http://localhost:5292${imagePath}`);
    }

    return encodeURI(`http://localhost:5292/${imagePath}`);
}