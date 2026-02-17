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
    const prevMaxParti = document.getElementById("prevMaxParti");
    const prevDate = document.getElementById("prevDate");
    const prevLocation = document.getElementById("prevLocation");

    const resetbutton = document.getElementById("btnRst");

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
            prevTitle.innerHTML = '(ชื่อกิจกรรมจะปรากฏที่นี่)';
            return;
        }
        prevTitle.innerHTML = this.value
    });

    inputDesc.addEventListener("input", function () {
        if (!this.value) {
            prevDesc.innerHTML = 'รายละเอียดต่างๆ ของกิจกรรม...';
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
            prevTags.innerHTML = '<span class="tag-badge">#ตัวอย่างแท็ก</span>';
            return;
        }
        let badges = tagText.split(',').map(tag => `<span class="tag-badge">#${tag.trim()}</span>`).join(" ");
        prevTags.innerHTML = badges;
    });

    inputMaxParti.addEventListener("input", function () {
        if (!this.value) {
            prevMaxParti.innerHTML = 'XX';
            return;
        }
        prevMaxParti.innerHTML = this.value
    });

    inputDateTime.addEventListener("input", function () {
        const rawDate = this.value;
        const dateObj = new Date(rawDate);
        if (!this.value) {
            prevDate.innerHTML = 'XX เดือน XXXX - XX:XX น.';
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
            prevLocation.innerHTML = 'ระบุสถานที่จัดงาน';
            return;
        }
        prevLocation.innerHTML = this.value
    });

    resetbutton.addEventListener("click", function () {
        if (inputTitle) inputTitle.value = originalData.Title;
        if (inputDesc) inputDesc.value = originalData.Desc;
        if (inputImg) inputImg.value = originalData.Img;
        if (inputTag) inputTag.value = originalData.Tag;
        if (inputMaxParti) inputMaxParti.value = originalData.MaxParti;
        if (inputDateTime) inputDateTime.value = originalData.DateTime;
        if (inputLocation) inputLocation.value = originalData.Location;

        const event = new Event('input');
        if (inputTitle) inputTitle.dispatchEvent(event);
        if (inputDesc) inputDesc.dispatchEvent(event);
        if (inputImg) inputImg.dispatchEvent(event);
        if (inputTag) inputTag.dispatchEvent(event);
        if (inputMaxParti) inputMaxParti.dispatchEvent(event);
        if (inputDateTime) inputDateTime.dispatchEvent(event);
        if (inputLocation) inputLocation.dispatchEvent(event);
    })
});