// script.js
const endDate = new Date('2024-12-31T23:59:59').getTime();

function updateCountdown() {
    const now = new Date().getTime();
    const timeRemaining = endDate - now;

    if (timeRemaining < 0) {
        document.getElementById('timer').innerHTML = 'ÇáæÞÊ ÇäÊåì!';
        return;
    }

    const days = Math.floor(timeRemaining / (1000 * 60 * 60 * 24));
    const hours = Math.floor((timeRemaining % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
    const minutes = Math.floor((timeRemaining % (1000 * 60 * 60)) / (1000 * 60));
    const seconds = Math.floor((timeRemaining % (1000 * 60)) / 1000);

    document.getElementById('days').textContent = formatTime(days);
    document.getElementById('hours').textContent = formatTime(hours);
    document.getElementById('minutes').textContent = formatTime(minutes);
    document.getElementById('seconds').textContent = formatTime(seconds);
}

function formatTime(value) {
    return value < 10 ? `0${value}` : value;
}

setInterval(updateCountdown, 1000);
