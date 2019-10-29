angular.module('umbraco.services').factory('ourUmbracoDeployAssistantService', function ($http) {

    var service = {

        reloadStatus: function () {
            return $http.get('/umbraco/backoffice/DeployAssistant/Status/Get');
        },

        reimport: function () {
            return $http.post('/umbraco/backoffice/DeployAssistant/Deploy/Import');
        },

        reloadSettings: function () {
            return $http.get('/umbraco/backoffice/DeployAssistant/Deploy/GetSettings');
        },

        exportUdas: function () {
            return $http.post('/umbraco/backoffice/DeployAssistant/Deploy/ExportUdas');
        }
    };
    
    return service;

});