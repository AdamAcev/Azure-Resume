window.addEventListener('DOMContentLoaded', () => {
  getVisitCount();
});

const productionApiUrl = 'https://azureresumevisitorcounter.azurewebsites.net/api/GetVisitorCounter';
const localApiUrl = 'http://localhost:7071/api/GetVisitorCounter';

const getVisitCount = () => {
  const isLocal =
    window.location.hostname === 'localhost' ||
    window.location.hostname === '127.0.0.1';

  const apiUrl = isLocal ? localApiUrl : productionApiUrl;

  fetch(apiUrl)
    .then((response) => response.json())
    .then((response) => {
      console.log('Website called function API.');
      document.getElementById('counter').innerText = response.count;
    })
    .catch((error) => {
      console.log(error);
    });
};
