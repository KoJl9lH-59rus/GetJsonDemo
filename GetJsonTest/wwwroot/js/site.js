document.getElementById('getButton').addEventListener('click', function () {
    const itemDiv = document.getElementById('item');
    const randomData = Math.floor(Math.random() * 100); // Генерация случайного числа
    itemDiv.textContent = Объект ID: ${ randomData };
});