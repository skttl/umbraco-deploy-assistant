angular.module('umbraco').controller('Our.Umbraco.DeployAssistant.DashboardController', function (notificationsService, ourUmbracoDeployAssistantService) {

    vm = this;

    vm.statusLoaded = false;

    vm.reloadStatus = function () {
        vm.statusLoading = true;

        ourUmbracoDeployAssistantService.reloadStatus().then(function (response) {
            vm.status = response.data;
            vm.statusLoaded = true;
            vm.statusLoading = false;
            vm.status.Content = JSON.parse(vm.status.Content);

            vm.status.LastEdit = moment(vm.status.LastEdit).toDate();
            
            if (vm.status.InProgress) {
                vm.reloadStatus();
            }
        });
    };

    vm.reloadStatus();

    var setProgressStatus = function () {
        if (vm.status) {

            vm.status.Complete = false;
            vm.status.Failed = false;
            vm.status.InProgress = true;
            vm.status.LastEdit = new Date();
            vm.status.Content = "";
        }
    };

    vm.reDeploy = function () {
        vm.statusLoading = true;

        ourUmbracoDeployAssistantService.reimport().then(function (response) {
            if (response.data) {

                // deploy marker created, set status as in progress
                setProgressStatus();

                // reload status, to get correct status
                vm.reloadStatus();
            }
            else {

                // deploy marker not created
                notificationsService.error("Failed", "Creation of deploy file failed!");
            }
        });
    };

    vm.exportUdas = function () {
        vm.statusLoading = true;

        ourUmbracoDeployAssistantService.exportUdas().then(function (response) {
            if (response.data) {

                // deploy marker created, set status as in progress
                setProgressStatus();
                vm.status.Export = true;

                // reload status, to get correct status
                vm.reloadStatus();
            }
            else {

                // deploy marker not created
                notificationsService.error("Failed", "Creation of deploy-export file failed!");
            }
        });
    };

    vm.settingsLoaded = false;

    vm.reloadSettings = function () {
        vm.settingsLoading = true;
        ourUmbracoDeployAssistantService.reloadSettings().then(function (response) {
            vm.settings = response.data;
            
            vm.settingsLoaded = true;
            vm.settingsLoading = false;
        });
    };

    vm.reloadSettings();
});