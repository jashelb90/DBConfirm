import React from 'react';

export default function QuickStart() {
    return (
        <>
            <h2 id="getting-started">Quick Start</h2>

            <p>The quickest (and recommended) way to get started is to install and run one of the DBConfirm template packages.  Alternatively, you can follow the <a href="/manualsetup">Manual Setup</a> guide.</p>
            <p>These template packages are provided to quickly set up new test projects, including all the depdendencies and required configuration.</p>
            <p>They are provided as NuGet packages, and need to be installed before new projects can be created.</p>

            <h3>Template package installation</h3>

            <div className="content-split">
                <div className="content-split-primary">
                    <p>To install the package (using MSTest as the test framework), execute this command from a command prompt:</p>
                    <pre>
                        <code>
                            dotnet new -i DBConfirm.Templates.SQLServer.MSTest
                        </code>
                    </pre>
                </div>
                <aside>
                    <header>Package for NUnit</header>
                    <div className="aside-body">
                        <p>To use NUnit as the test framework, use <code>DBConfirm.Templates.SQLServer.NUnit</code> instead.</p>
                    </div>
                </aside>
            </div>

            <h3>Creating a new project</h3>
            <div className="content-split">
                <div className="content-split-primary">
                    <p>Once installed, navigate to the directory where you want to add the new project, and execute this command from a command prompt:</p>
                    <pre>
                        <code>
                            dotnet new dbconfirm-sqlserver-mstest
                </code>
                    </pre>

                    <p>If you want to use a custom name for the new project, supply the <code>-n NewProjectName</code> argument, to set the name.</p>
                </div>
                <aside>
                    <header>Command for NUnit</header>
                    <div className="aside-body">
                        <p>To add an NUnit test project, use <code>dbconfirm-sqlserver-nunit</code> instead.</p>
                    </div>
                </aside>
            </div>

            <p>This will add a new project, with the required NuGet packages installed.  There will be a sample test class (inheriting from the correct base class),
            containing a single test method (already marked as async).  There will also be an appsettings.json file with a default connection string,
            which will need to be updated to point to your own database.
            </p>

            <h3>Upating the default connection string</h3>

            <p>DBConfirm requires a database instance to run its tests against.  This database needs to have the correct schema, but it is recommended to use a database
            that contains no data (or only static data that never changes) - all test data will be added as part of the tests themselves.
    </p>

            <p>In the root of your test project, find the <strong>appsettings.json</strong> file.  This file will contain
                    a connection string called '<strong>DefaultConnectionString</strong>', which is used to point to the database to be tested:</p>
            <pre><code className="lang-json">{"{"}
                {"\n"}  <span className="hljs-attr">"ConnectionStrings"</span>: {"{"}
                {"\n"}    <span className="hljs-attr">"DefaultConnectionString"</span>: <span className="hljs-string">"SERVER=(local);DATABASE=TestDatabaseName;Integrated Security=true;Connection Timeout=30;"</span>
                {"\n"}  {"}"}
                {"\n"}{"}"}
            </code></pre>

            <p>This connection string needs to be updated to point to the database that is to be used whilst running the tests.</p>

            <p>For more details on how to set the connection strings, see the <a href="/connectionstrings">Connection Strings</a> guide.</p>

            <h3>Writing your first test</h3>

            <p>Congratulations, you're now ready to start writing tests.</p>

            <p>For the full details of how to insert data, execute database logic and verify results, see the <a
                href="/writingtests">Writing Tests</a> guide.</p>
        </>
    );
}