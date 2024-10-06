using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

var options = new ChromeOptions();
options.AddArgument("--window-size=1200,900");

IWebDriver chromeDriver = new ChromeDriver(options);

// Starting test
await StartAutomatedTest();


async Task StartAutomatedTest()
{
    try
    {
        Console.Clear();

        // i
        chromeDriver.Navigate().GoToUrl("https://playground.tensorflow.org");

        Actions action = new Actions(chromeDriver);
        WaitUntilElementExists("top-controls", 10);

        // ii
        PrintTestLossValue();

        // iii
        ChangeDatasetToExlusive();

        // iv
        ChangeNoiseToFive(action);

        // v
        SelectFeature("//*[@id=\"canvas-xSquared\"]/div/canvas");
        SelectFeature("//*[@id=\"canvas-ySquared\"]/div/canvas");

        // vi
        RemoveNeuron("//*[@id=\"network\"]/div[17]/div[1]/button[2]");
        RemoveNeuron("//*[@id=\"network\"]/div[17]/div[1]/button[2]");

        // vii
        ChangeLearningRate("0.1");

        // viii
        PressPlayPauseBtn();

        // ix
        await Task.Delay(3000);
        PressPlayPauseBtn();

        // x
        PrintTestLossValue();

        //Exit chrome automatically?
        //chromeDriver.Quit();
    }
    catch (Exception ex)
    {
        chromeDriver.Quit();
        Console.WriteLine(ex.ToString());
    }

    Console.WriteLine($"\nPress any key to exit...");
    Console.ReadLine();
}




void WaitUntilElementExists(string element, int seconds)
{
    WebDriverWait wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(seconds));
    wait.Until(ExpectedConditions.ElementExists(By.Id(element)));
}

void PrintTestLossValue()
{
    IWebElement testLossElement = chromeDriver.FindElement(By.Id("loss-test"));
    Console.WriteLine($"Test loss value: {testLossElement.Text}\n");
}

void ChangeDatasetToExlusive()
{
    IWebElement datasetElement = chromeDriver.FindElement(By.XPath("//*[@id=\"main-part\"]/div[1]/div[1]/div/div[2]/canvas"));
    datasetElement.Click();
}

void ChangeNoiseToFive(Actions action)
{
    IWebElement noiseSlider = chromeDriver.FindElement(By.Id("noise"));
    action.DragAndDropToOffset(noiseSlider, -40, 0).Perform();
}

void SelectFeature(string XPath)
{
    IWebElement feature1 = chromeDriver.FindElement(By.XPath(XPath));
    feature1.Click();
}

void RemoveNeuron(string XPath)
{
    IWebElement neuronBtn1 = chromeDriver.FindElement(By.XPath(XPath));
    neuronBtn1.Click();
}

void ChangeLearningRate(string value)
{
    IWebElement learningRateDropdown = chromeDriver.FindElement(By.Id("learningRate"));
    SelectElement learningRateSelect = new SelectElement(learningRateDropdown);
    learningRateSelect.SelectByText(value);
}

void PressPlayPauseBtn()
{
    IWebElement playPauseBtn = chromeDriver.FindElement(By.Id("play-pause-button"));
    playPauseBtn.Click();
}