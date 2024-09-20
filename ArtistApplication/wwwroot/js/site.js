// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// wwwroot/js/custom-audio-player.js
// wwwroot/js/custom-audio-player.js
document.addEventListener('DOMContentLoaded', function () {
    const audioPlayers = document.querySelectorAll('.custom-audio-player');

    audioPlayers.forEach(player => {
        const audioFileUrl = player.getAttribute('data-audio-url');
        const playPauseBtn = player.querySelector('.play-pause-btn');
        const seekBar = player.querySelector('.seek-bar');
        const currentTimeEl = player.querySelector('.current-time');
        const durationEl = player.querySelector('.duration');
        const volumeBar = player.querySelector('.volume-bar');

        const audio = new Audio(audioFileUrl);

        // Play/Pause toggle with icon change
        playPauseBtn.addEventListener('click', () => {
            if (audio.paused) {
                audio.play();
                playPauseBtn.innerHTML = '<i class="bi bi-pause-fill"></i>';
            } else {
                audio.pause();
                playPauseBtn.innerHTML = '<i class="bi bi-play-fill"></i>';
            }
        });

        // Update seek bar as audio plays
        audio.addEventListener('timeupdate', () => {
            const currentTime = audio.currentTime;
            const duration = audio.duration;
            if (!isNaN(duration)) {
                seekBar.value = (currentTime / duration) * 100;
                updateCurrentTime();
            }
        });

        // Seek bar change
        seekBar.addEventListener('input', () => {
            const duration = audio.duration;
            if (!isNaN(duration)) {
                audio.currentTime = (seekBar.value / 100) * duration;
            }
        });

        // Volume change
        volumeBar.addEventListener('input', () => {
            audio.volume = volumeBar.value / 100;
        });

        // Update current time display
        function updateCurrentTime() {
            const currentTime = audio.currentTime;
            currentTimeEl.textContent = formatTime(currentTime);
        }

        // Format time to mm:ss
        function formatTime(seconds) {
            const minutes = Math.floor(seconds / 60);
            const secs = Math.floor(seconds % 60);
            return `${minutes}:${secs < 10 ? '0' : ''}${secs}`;
        }

        // Display audio duration once metadata is loaded
        audio.addEventListener('loadedmetadata', () => {
            durationEl.textContent = formatTime(audio.duration);
        });
    });
});
