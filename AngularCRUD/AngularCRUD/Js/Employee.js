var app = angular.module("myapp", []);
app.controller("myCtrl", function ($scope, $http, $window) {


    //GET:-
    $scope.getAllData = function () {
        $http({
            method: "GET",
            url: "http://localhost:51583/Employee/GetAllEmployee"
        }).then(function (response) {
            $scope.employees = response.data;
        }, function () {
            alert("Error Occured");
        })
        
    }


    //Insert:-
    $scope.InsertData = function () {
        var Action = document.getElementById("btnSave").getAttribute("value");
        if (Action == "Submit") {
        $scope.Employee = {};
        $scope.Employee.emp_name = $scope.empName;
        $scope.Employee.emp_city = $scope.empCity;
        $scope.Employee.emp_age = $scope.empAge;
        $http({
            method: "post",
            url: "http://localhost:51583/Employee/Insert_Employee",
            datatype: "json",
            data: JSON.stringify($scope.Employee)
        }).then(function (response) {
            alert(response.data);
            //$scope.GetAllData();
            $scope.empName = "";
            $scope.empCity = "";
            $scope.empAge = "";
            //$scope.Employee.$setPristine();
            //$scope.Employee.$setUntouched();    
            window.location.reload();
        })
        }
        else {
            $scope.Employee = {};
            $scope.Employee.emp_name = $scope.empName;
            $scope.Employee.emp_city = $scope.empCity;
            $scope.Employee.emp_age = $scope.empAge;
            $scope.Employee.emp_id = $scope.empId;

                $http({
                    method: "post",
                    url: "http://localhost:51583/Employee/Update_Employee",
                    datatype: "json",
                    data: JSON.stringify($scope.Employee)
                }).then(function (response) {
                    alert(response.data);
                    $scope.empName = "";
                    $scope.empAge = "";
                    $scope.empCity = "";
                    document.getElementById("btnSave").setAttribute("value", "Submit");
                    document.getElementById("spn").innerHTML = "Add New Employee";
                    window.location.reload();
                })

        }
    }

    //UPDATE:-
    $scope.UpdateData = function (Emp) {
        console.log("------->getting element====>",Emp.emp_id);
        //document.getElementById("EmpId_").value = Emp.emp_id;
        $scope.empId = Emp.emp_id;
        $scope.empName = Emp.emp_name;
        $scope.empAge = Emp.emp_age;
        $scope.empCity = Emp.emp_city;
        document.getElementById("btnSave").setAttribute("value", "Update");
        //document.getElementById("btnSave").style.backgroundColor = "Yellow";
        document.getElementById("spn").innerHTML = "Update Employee Information";

    }


    //DELETE:-
    $scope.DeleteData = function (Emp) {
        //$scope.Employee = {};
        //$scope.Employee.emp_id = Emp.emp_id;
        //$scope.Employee.emp_name
        $http({
            method: "post",
            url: "http://localhost:51583/Employee/Delete_Employee",
            datatype: "json",
            data: JSON.stringify(Emp)
        }).then(function (response) {
            alert(response.data);
            window.location.reload();
        })
    }
})