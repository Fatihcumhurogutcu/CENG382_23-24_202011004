document.getElementById('calculate-button').addEventListener('click', function() {
    var firstNumber = document.getElementById('first-number').value;
    var secondNumber = document.getElementById('second-number').value;
    var sum = parseInt(firstNumber, 10) + parseInt(secondNumber, 10);
    document.getElementById('result').textContent = 'The sum of ' + firstNumber + ' and ' + secondNumber + ' is: ' + sum;
  });
  
  document.getElementById('toggle-button').addEventListener('click', function() {
    var content = document.getElementById('content');
    if (content.style.display === 'none' || content.style.display === '') {
      content.style.display = 'block';
    } else {
      content.style.display = 'none';
    }
  });
  