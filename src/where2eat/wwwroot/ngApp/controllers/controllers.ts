namespace where2eat.Controllers {

    export class HomeController {
        public message = 'You can use this app to help you decide where you should eat.  Login and follow the instruction on each page.'
    }

    export class EventController {
        public message = 'Add an event to your list by entering the information below';
        public EventResource;
        public newEvent;
        public currentUserName;

        public getUserName() {
            this.currentUserName = this.accountService.getUserName();
            console.log(this.currentUserName);
        }

        public addEvent() {
            this.newEvent.eventStatus = true;
            console.log(this.newEvent);
            this.currentUserName = this.accountService.getUserName();
            console.log(this.currentUserName);
            this.EventResource.save({ id: this.currentUserName }, this.newEvent).$promise;
            this.newEvent = null;
        }

        constructor(public $state: ng.ui.IStateService, public accountService: where2eat.Services.AccountService, public $resource: angular.resource.IResourceService) {
            this.EventResource = $resource('/api/events/:id');
            this.getUserName();
        }
    }

    export class OptionController {
        public message = 'Follow the steps below:';
        public EventResource;
        public OptionResource;
        public DecisionResource;
        public currentUserName;
        public events;
        public selectedEventOptions;
        public selectedEvent;
        public newOption;
        public optionToUpdate;
        public optionToDelete;
        public winningDecision;

        public getEvents() {
            this.currentUserName = this.accountService.getUserName();
            this.events = this.EventResource.query({ id: this.currentUserName });
            console.log(this.events);
        }

        public getOptions() {
            console.log(this.selectedEvent);
            this.selectedEventOptions = this.OptionResource.query({ id: this.selectedEvent.id });
            console.log(this.selectedEventOptions);
         }

        public addOption(newOption) {
            this.newOption = newOption;
            this.newOption.OptionValue = Math.random();
            console.log(this.newOption);
            this.OptionResource.save({ id: this.selectedEvent.id }, this.newOption).$promise.then(() => {
                this.getOptions();
                this.newOption.OptionName = null;
                this.newOption.OptionDescription = null;
                this.newOption.OptionContributor = null;
                this.newOption.OptionValue = null;
            });
        }

        public updateOption(option) {
            this.optionToUpdate = option;
            console.log(this.optionToUpdate);
            this.OptionResource.save(this.optionToUpdate).$promise.then(() => {
                this.getOptions();
            });
        }

        public deleteOption(option) {
            this.optionToDelete = option;
            this.OptionResource.delete({ id: this.optionToDelete.id }).$promise.then(() => {
                this.getOptions();
            });
        }

       
        constructor(public $state: ng.ui.IStateService, private accountService: where2eat.Services.AccountService, private $resource: angular.resource.IResourceService, private $uibModal: angular.ui.bootstrap.IModalService) {
            this.EventResource = $resource('/api/events/:id');
            this.OptionResource = $resource('/api/options/:id');
            this.getEvents();
        }
    }

    export class DecisionController {
    
          
        }

    }

