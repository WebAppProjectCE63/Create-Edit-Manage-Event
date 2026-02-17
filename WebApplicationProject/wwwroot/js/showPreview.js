let originalData = {};
document.addEventListener("DOMContentLoaded", function () {
    const inputTitle = document.getElementById("inputTitle");
    const inputDesc = document.getElementById("inputDesc");
    const inputImg = document.getElementById("inputImg");
    const inputTag = document.getElementById("inputTag");
    const inputMaxParti = document.getElementById("inputMaxParti");
    const inputDateTime = document.getElementById("inputDateTime");
    const inputLocation = document.getElementById("inputLocation");

    const prevImage = document.getElementById("prevImage");
    const prevTitle = document.getElementById("prevTitle");
    const prevDesc = document.getElementById("prevDesc");
    const prevTags = document.getElementById("prevTags");
    const prevCount = document.getElementById("prevCount");
    const prevDate = document.getElementById("prevDate");
    const prevLocation = document.getElementById("prevLocation");
})
originalData = {
    Title: inputTitle.value,
    Desc: inputDesc.value,
    Img: inputImg.value,
    Tag: inputTag.value,
    MaxParti: inputMaxParti.value,
    DateTime: inputDateTime.value,
    Location: inputLocation.value,
};

inputTitle.addEventListener("input", function () {
    if (!this.value) {
        prevTitle.innerHTML = '-';
        return;
    }
    prevTitle.innerHTML = this.value
});

inputDesc.addEventListener("input", function () {
    if (!this.value) {
        prevDesc.innerHTML = '-';
        return;
    }
    prevDesc.innerHTML = this.value
});

inputImg.addEventListener("input", function () {
    if (!this.value) {
        prevImage.src = "https://img2.pic.in.th/image-icon-symbol-design-illustration-vector.md.jpg";
        return;
    }
    prevImage.src = this.value
});

inputTag.addEventListener("input", function () {
    let tagText = this.value
    if (!tagText) {
        prevTags.innerHTML = '-';
        return;
    }
    let badges = tagText.split(',').map(tag => `<span class="tag-badge">#${tag.trim()}</span>`).join(" ");
    prevTags.innerHTML = badges;
});

inputMaxParti.addEventListener("input", function () {
    if (!this.value) {
        prevCount.innerHTML = '-';
        return;
    }
    prevCount.innerHTML = this.value
});

inputDateTime.addEventListener("input", function () {
    const rawDate = this.value;
    const dateObj = new Date(rawDate);
    if (!this.value) {
        prevDate.innerHTML = '-';
        return;
    }
    const datePart = dateObj.toLocaleDateString('th-TH', {
        day: 'numeric',
        month: 'long',
        year: 'numeric'
    });
    const timePart = dateObj.toLocaleTimeString('th-TH', {
        hour: '2-digit',
        minute: '2-digit'
    });
    prevDate.innerHTML = `${datePart} - ${timePart} น.`
});

inputLocation.addEventListener("input", function () {
    if (!this.value) {
        prevLocation.innerHTML = '-';
        return;
    }
    prevLocation.innerHTML = this.value
});