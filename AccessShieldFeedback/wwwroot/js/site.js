let selectedRating = 0;
const starsContainer = document.getElementById("stars");
const responseMsg = document.getElementById("responseMessage");
const feedbackList = document.getElementById("feedback-list");

function renderStars() {
    for (let i = 1; i <= 5; i++) {
        const star = document.createElement("span");
        star.innerText = "★";
        star.className = "text-3xl cursor-pointer transition transform";
        star.dataset.value = i;

        star.addEventListener("mouseover", () => highlightStars(i));
        star.addEventListener("mouseout", () => highlightStars(selectedRating));
        star.addEventListener("click", () => {
            selectedRating = i;
            highlightStars(selectedRating);
        });

        starsContainer.appendChild(star);
    }
}

function highlightStars(count) {
    const stars = starsContainer.querySelectorAll("span");
    stars.forEach((star, i) => {
        if (i < count) {
            star.classList.add("text-yellow-400");
            star.classList.remove("text-gray-400");
        } else {
            star.classList.remove("text-yellow-400");
            star.classList.add("text-gray-400");
        }
    });
}

document.getElementById("submit").addEventListener("click", async () => {
    const name = document.getElementById("name").value.trim();
    const message = document.getElementById("message").value.trim();
    const address = document.getElementById("address").value;

    if (!name || !message || selectedRating === 0) {
        showResponse("Please fill in all the fields and select a rating!", "text-red-600");
        return;
    }

    try {
        const res = await fetch("https://localhost:7021/Home/PostFeedback/", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ username: name, address: address, message: message, rating: selectedRating }),
        });

        if (res.ok) {
            showResponse("The feedback has been successfully submitted! Thank you!", "text-green-600");
            loadFeedback();

            document.getElementById("name").value = "";
            document.getElementById("message").value = "";
            document.getElementById("address").value = "";

            selectedRating = 0;
            highlightStars(0);
        } else {
            showResponse("Error while submitting. Please try again.", "text-red-600");
        }
    } catch (err) {
        showResponse("The server is not responding. Please check your connection.", "text-red-600");
    }
});

function showResponse(msg, className) {
    responseMsg.textContent = msg;
    responseMsg.className = `mt-4 text-sm font-medium ${className}`;
}

function formatDate(date) {
    var hours = date.getHours();
    var minutes = date.getMinutes();

    var ampm = hours >= 12 ? 'PM' : 'AM';
    hours = hours % 12;

    hours = hours ? hours : 12;
    minutes = minutes < 10 ? '0' + minutes : minutes;
    var strTime = hours + ':' + minutes + ' ' + ampm;
    return date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear() + " " + strTime;
}

async function loadFeedback() {
    feedbackList.innerHTML = "";

    try {
        const res = await fetch("https://localhost:7021/Home/GetAllFeedbacks/");
        const data = await res.json();
        
        data.feedbackViewModels.reverse().forEach(fb => {
            const div = document.createElement("div");
            div.className = "bg-gray-100 rounded p-4";

            div.innerHTML = `
        <h3 class="text-lg font-bold text-green-700">${fb.username}</h3>
        <p class="text-gray-700 mb-1">${fb.message}</p>
        <span class="text-yellow-500 text-xl">${"★".repeat(fb.rating)}${"☆".repeat(5 - fb.rating)}</span>
        <span class="text-500 text-sm text-nicier">${formatDate(new Date(fb.createdAt))}</span>
      `;

            feedbackList.appendChild(div);
        });
    } catch (err) {
        feedbackList.innerHTML = "<p class='text-red-600'>Could not load the feedbacks.</p>";
    }
}

renderStars();
highlightStars(0);
loadFeedback();