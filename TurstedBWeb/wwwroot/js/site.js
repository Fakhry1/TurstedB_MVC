
const stepButtons = document.querySelectorAll('.step-button');
const progress = document.querySelector('#progress');
var form = document.getElementById("form-id");
var step = @State;

Array.from(stepButtons).forEach((button, index) => {
    if (index < step) { button.classList.add('done') }
    progress.setAttribute('value', step * 100 / (stepButtons.length - 1));
});
Array.from(stepButtons).forEach((button, index) => {
    button.addEventListener('click', () => {
        progress.setAttribute('value', index * 100 / (stepButtons.length - 1));//there are 3 buttons. 2 spaces.
        //stepButtons.classList.add('done');
        stepButtons.forEach((item, secindex) => {

            if (index >= secindex) {
                item.classList.add('done');
                //document.getElementById("your-id").addEventListener("click", function () {
                //    form.submit();
                //});

            }
            if (index < secindex) {
                item.classList.remove('done');
                //document.getElementById("your-id").addEventListener("click", function () {
                //    form.submit();
                //});
            }
        })
    })
});
