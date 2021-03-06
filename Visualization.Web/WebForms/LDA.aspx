﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LDA.aspx.cs" Inherits="Webforms_LDA" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LDA</title>
    <script type="text/javascript" src="../Scripts/d3.v3.min.js"></script>
    <script type="text/javascript" src="../Scripts/xregexp-all-min.js"></script>
    <script type="text/javascript" src="../Scripts/queue.min.js"></script>

    <style>
        body
        {
            font-family: Calibri, Verdana;
            background-color: #333;
        }

        div#buttons
        {
            background-color: #ddd;
            width: 70%;
        }

        div.sidebar
        {
            float: right;
            width: 25%;
        }

            div.sidebar div.sidebox
            {
                background-color: #ddd;
                margin: 5px;
                padding: 5px;
                -moz-border-radius: 4px;
                border-radius: 4px;
            }

        div#topics div
        {
            border-bottom: solid #aaa 1px;
            padding: 5px;
        }

            div#topics div.selected
            {
                background-color: #ddf;
                font-weight: bold;
            }

        .page
        {
            padding: 10px 10px 25px;
            background-color: #fff;
            margin-top: 4px;
            display: none;
        }

        div#docs-page
        {
            display: block;
        }

        div.document
        {
            margin: 10px;
            padding: 10px;
        }

            div.document:nth-child(2n)
            {
                background-color: #eee;
            }

        text
        {
            font-size: small;
            fill: #555;
            cursor: default;
            -webkit-touch-callout: none;
            -webkit-user-select: none;
            -khtml-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

        #tabwrapper
        {
            width: 70%;
        }

        .tabs
        {
            height: 30px;
        }

            .tabs > ul
            {
                font-size: 1em;
                list-style: none;
            }

                .tabs > ul > li
                {
                    margin: 0 2px 0 0;
                    padding: 7px 10px;
                    display: block;
                    float: left;
                    color: #FFF;
                    -webkit-user-select: none;
                    -moz-user-select: none;
                    user-select: none;
                    -moz-border-radius-topleft: 4px;
                    -moz-border-radius-topright: 4px;
                    -moz-border-radius-bottomright: 0px;
                    -moz-border-radius-bottomleft: 0px;
                    border-top-left-radius: 4px;
                    border-top-right-radius: 4px;
                    border-bottom-right-radius: 0px;
                    border-bottom-left-radius: 0px;
                    background: #C9C9C9; /* old browsers */
                    background: -moz-linear-gradient(top, #0C91EC 0%, #257AB6 100%); /* firefox */
                    background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#0C91EC), color-stop(100%,#257AB6)); /* webkit */
                }

                    .tabs > ul > li:hover
                    {
                        background: #FFFFFF; /* old browsers */
                        background: -moz-linear-gradient(top, #FFFFFF 0%, #F3F3F3 10%, #F3F3F3 50%, #FFFFFF 100%); /* firefox */
                        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#FFFFFF), color-stop(10%,#F3F3F3), color-stop(50%,#F3F3F3), color-stop(100%,#FFFFFF)); /* webkit */
                        cursor: pointer;
                        color: #333;
                    }

                    .tabs > ul > li.selected
                    {
                        background: #FFFFFF; /* old browsers */
                        background: -moz-linear-gradient(top, #FFFFFF 0%, #F3F3F3 10%, #F3F3F3 50%, #FFFFFF 100%); /* firefox */
                        background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#FFFFFF), color-stop(10%,#F3F3F3), color-stop(50%,#F3F3F3), color-stop(100%,#FFFFFF)); /* webkit */
                        cursor: pointer;
                        color: #333;
                    }
    </style>
</head>
<body>
    <div id="main">
        <div class="sidebar">
            <div id="form" class="sidebox">
                <form>
                    <div>Documents URL:
                        <input id="docs-url-input" type="text" name="docs" value="" /></div>
                    <div>Stoplist URL:
                        <input id="stops-url-input" type="text" name="stoplist" value="" /></div>
                    # Topics:
                    <input id="num-topics-input" type="text" name="topics" value="" />
                    <input type="submit" value="Load" />
                </form>
            </div>

            <div class="sidebox">
                <button id="sweep">Run 50 iterations</button>
                Iterations: <span id="iters">0</span>
            </div>

            <div id="topics" class="sidebox">
                <h3>
                Topics [Click to sort documents]
            </div>
        </div>
    </div>

    <div id="tabwrapper">
        <div class="tabs">
            <ul>
                <li id="docs-tab" class="selected">Documents</li>
                <li id="vocab-tab">Vocabulary</li>
                <li id="corr-tab">Topic Correlations</li>
            </ul>
        </div>
        <div id="pages">

            <div id="docs-page" class="page">
            </div>

            <div id="vocab-page" class="page">
                <table id="vocab-table">
                    <thead>
                        <th>Word</th>
                        <th>Frequency</th>
                    </thead>
                    <tbody></tbody>
                </table>
            </div>

            <div id="corr-page" class="page">
                <div>Correlation threshold: low
                    <input id="corr-slider" type="range" min="0" max="50" />
                    high</div>
            </div>

        </div>
    </div>

    <script>

        /** This function is copied from stack overflow: http://stackoverflow.com/users/19068/quentin */
        var QueryString = function () {
            // This function is anonymous, is executed immediately and 
            // the return value is assigned to QueryString!
            var query_string = {};
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                // If first entry with this name
                if (typeof query_string[pair[0]] === "undefined") {
                    query_string[pair[0]] = pair[1];
                    // If second entry with this name
                } else if (typeof query_string[pair[0]] === "string") {
                    var arr = [query_string[pair[0]], pair[1]];
                    query_string[pair[0]] = arr;
                    // If third or later entry with this name
                } else {
                    query_string[pair[0]].push(pair[1]);
                }
            }
            return query_string;
        }();

        var documentTopicSmoothing = 0.1;
        var topicWordSmoothing = 0.01;

        var vocabularySize = 0;

        var vocabularyCounts = {};

        var numTopics = QueryString.topics ? parseInt(QueryString.topics) : 25;
        if (isNaN(numTopics)) {
            alert("The requested number of topics [" + QueryString.topics + "] couldn't be interpreted as a number");
            numTopics = 25;
        }
        var documentsURL = QueryString.docs ? QueryString.docs : "sotu_small.txt";
        var stopwordsURL = QueryString.stoplist ? QueryString.stoplist : "sotu_stop.txt";

        d3.select("#docs-url-input").attr("value", documentsURL);
        d3.select("#stops-url-input").attr("value", stopwordsURL);
        d3.select("#num-topics-input").attr("value", numTopics);

        var stopwords = {};
        //  ["the", "and", "of", "for", "in", "a", "on", "is", "an", "this", "to", "by", "abstract", "paper", "based", "with", "or", "are", "from", "upon", "we", "us", "our", "can", "be", "using", "which", "that", "d", "n", "as", "it", "show", "these", "such", "s", "t", "i", "j", "have", "one", "new", "one", "has", "learning", "model", "data", "models", "two", "used", "results"].forEach( function(d) { stopwords[d] = 1; } );

        var docSortSmoothing = 10.0;
        var sumDocSortSmoothing = docSortSmoothing * numTopics;

        var completeSweeps = 0;

        var selectedTopic = -1;

        var wordTopicCounts = {};
        var topicWordCounts = [];
        var tokensPerTopic = [];
        tokensPerTopic.length = numTopics;
        for (var topic = 0; topic < numTopics; topic++) {
            tokensPerTopic[topic] = 0;
        }

        var topicWeights = [];
        topicWeights.length = numTopics;

        var documents = [];

        /* SVG functions */
        var w = 650,
            h = 400,
            fill = d3.scale.category20();

        var vis = d3.select("#corr-page")
            .append("svg:svg")
              .attr("width", w)
              .attr("height", h);

        var linkDistance = 150;
        var correlationCutoff = 0.25;

        var truncate = function (s) { return s.length > 300 ? s.substring(0, 299) + "..." : s; }

        var parseLine = function (line) {
            if (line == "") { return; }
            var docID = documents.length;
            var docDate = "";
            var fields = line.split("\t");
            var text = fields[0];  // Assume there's just one field, the text
            if (fields.length == 3) {  // If it's in [ID]\t[TAG]\t[TEXT] format...
                docID = fields[0];
                docDate = fields[1];
                text = fields[2];
            }

            var tokens = [];
            var rawTokens = text.toLowerCase().match(XRegExp("\\p{L}[\\p{L}\\p{P}]*\\p{L}", "g"));
            if (rawTokens == null) { return; }
            var topicCounts = new Array(numTopics);
            for (var topic = 0; topic < numTopics; topic++) { topicCounts[topic] = 0; }

            rawTokens.forEach(function (word) {
                if (word !== "" && !stopwords[word] && word.length > 2) {
                    var topic = Math.floor(Math.random() * numTopics);
                    tokensPerTopic[topic]++;
                    if (!wordTopicCounts[word]) {
                        wordTopicCounts[word] = {};
                        vocabularySize++;
                        vocabularyCounts[word] = 0;
                    }
                    if (!wordTopicCounts[word][topic]) {
                        wordTopicCounts[word][topic] = 0;
                    }
                    wordTopicCounts[word][topic] += 1;
                    vocabularyCounts[word] += 1;
                    topicCounts[topic] += 1;
                    tokens.push({ "word": word, "topic": topic });
                }
            });

            documents.push({ "originalOrder": documents.length, "id": docID, "date": docDate, "originalText": text, "tokens": tokens, "topicCounts": topicCounts });
            d3.select("div#docs-page").append("div")
               .attr("class", "document")
               .text("[" + docID + "] " + truncate(text));
        };

        var sampleDiscrete = function (weights, sum) {
            var sample = sum * Math.random();
            var i = 0;
            sample -= weights[i];
            while (sample > 0.0) {
                i++;
                sample -= weights[i];
            }
            return i;
        }

        var sweep = function () {
            documents.forEach(function (currentDoc, i) {
                var docTopicCounts = currentDoc.topicCounts;
                for (var position = 0; position < currentDoc.tokens.length; position++) {
                    var token = currentDoc.tokens[position];
                    tokensPerTopic[token.topic]--;
                    var currentWordTopicCounts = wordTopicCounts[token.word];
                    currentWordTopicCounts[token.topic]--;
                    docTopicCounts[token.topic]--;

                    var sum = 0;
                    for (var topic = 0; topic < numTopics; topic++) {
                        if (currentWordTopicCounts[topic]) {
                            topicWeights[topic] =
                              (documentTopicSmoothing + docTopicCounts[topic]) *
                              (topicWordSmoothing + currentWordTopicCounts[topic]) /
                              (vocabularySize * topicWordSmoothing + tokensPerTopic[topic]);
                        }
                        else {
                            topicWeights[topic] =
                              (documentTopicSmoothing + docTopicCounts[topic]) * topicWordSmoothing /
                              (vocabularySize * topicWordSmoothing + tokensPerTopic[topic]);
                        }
                        sum += topicWeights[topic];
                    }

                    token.topic = sampleDiscrete(topicWeights, sum);
                    tokensPerTopic[token.topic]++;
                    if (!currentWordTopicCounts[token.topic]) {
                        currentWordTopicCounts[token.topic] = 1;
                    }
                    else {
                        currentWordTopicCounts[token.topic] += 1;
                    }
                    docTopicCounts[token.topic]++;
                }
            });

            completeSweeps += 1;
            d3.select("#iters").text(completeSweeps);
            console.log("sweep " + completeSweeps);
        }

        var byCountDescending = function (a, b) { return b.count - a.count; };
        var topNWords = function (wordCounts, n) { return wordCounts.slice(0, n).map(function (d) { return d.word; }).join(" "); };

        var sortTopicWords = function () {
            topicWordCounts = [];
            for (var topic = 0; topic < numTopics; topic++) {
                topicWordCounts[topic] = [];
            }

            for (var word in wordTopicCounts) {
                for (var topic in wordTopicCounts[word]) {
                    topicWordCounts[topic].push({ "word": word, "count": wordTopicCounts[word][topic] });
                }
            }

            for (var topic = 0; topic < numTopics; topic++) {
                topicWordCounts[topic].sort(byCountDescending);
            }
        };

        var displayTopicWords = function () {
            var topicTopWords = [];

            for (var topic = 0; topic < numTopics; topic++) {
                topicTopWords.push(topNWords(topicWordCounts[topic], 10));
            }

            var topicLines = d3.select("div#topics").selectAll("div")
              .data(topicTopWords);

            topicLines
              .enter().append("div")
              .attr("class", "topicwords")
              .on("click", function (d, i) { toggleTopicDocuments(i); });

            topicLines.transition().text(String);

            return topicWordCounts;
        };

        var reorderDocuments = function () {
            if (selectedTopic === -1) {
                documents.sort(function (a, b) { return d3.ascending(a.originalOrder, b.originalOrder); });
                d3.selectAll("div.document").data(documents)
                  .style("display", "block")
                  .text(function (d) { return "[" + d.id + "] " + truncate(d.originalText); });
            }
            else {
                documents.sort(function (a, b) {
                    var score1 = (a.topicCounts[selectedTopic] + docSortSmoothing) / (a.tokens.length + sumDocSortSmoothing);
                    var score2 = (b.topicCounts[selectedTopic] + docSortSmoothing) / (b.tokens.length + sumDocSortSmoothing);
                    return d3.descending(score1, score2);
                });
                d3.selectAll("div.document").data(documents)
                  .style("display", function (d) { return d.topicCounts[selectedTopic] > 0 ? "block" : "none"; })
                  .text(function (d) { return "[" + d.id + "] " + truncate(d.originalText); });
            }
        }

        var getTopicCorrelations = function () {
            // initialize the matrix
            correlationMatrix = new Array(numTopics);
            for (var t1 = 0; t1 < numTopics; t1++) {
                correlationMatrix[t1] = new Array(numTopics);
                for (var t2 = 0; t2 < numTopics; t2++) {
                    correlationMatrix[t1][t2] = 0.0;
                }
            }

            var documentLogProbabilities = new Array(documents.length);
            var meanLogProbabilities = new Array(numTopics);
            for (var topic = 0; topic < numTopics; topic++) { meanLogProbabilities[topic] = 0.0; }
            // iterate once to get mean log topic proportions
            documents.forEach(function (d, i) {
                documentLogProbabilities[i] = new Array(numTopics);
                for (var topic = 0; topic < numTopics; topic++) {
                    documentLogProbabilities[i][topic] = Math.log((d.topicCounts[topic] + documentTopicSmoothing) /
                                                            (d.tokens.length + numTopics * documentTopicSmoothing));
                    meanLogProbabilities[topic] += documentLogProbabilities[i][topic];
                }
            });
            for (var topic = 0; topic < numTopics; topic++) {
                meanLogProbabilities[topic] /= documents.length;
            }

            // iterate through the documents
            for (var doc = 0; doc < documents.length; doc++) {
                var logProbabilities = documentLogProbabilities[doc];
                for (var t1 = 0; t1 < numTopics; t1++) {
                    for (var t2 = 0; t2 < numTopics; t2++) {
                        correlationMatrix[t1][t2] += (logProbabilities[t1] - meanLogProbabilities[t1]) *
                                              (logProbabilities[t2] - meanLogProbabilities[t2]);
                    }
                }
            }

            var normalizer = 1.0 / (documents.length - 1);
            for (var t1 = 0; t1 < numTopics; t1++) {
                for (var t2 = 0; t2 < numTopics; t2++) {
                    correlationMatrix[t1][t2] *= normalizer;
                }
            }
            var standardDeviations = new Array(numTopics);
            for (var topic = 0; topic < numTopics; topic++) {
                standardDeviations[topic] = Math.sqrt(correlationMatrix[topic][topic]);
            }

            for (var t1 = 0; t1 < numTopics; t1++) {
                for (var t2 = 0; t2 < numTopics; t2++) {
                    correlationMatrix[t1][t2] /= (standardDeviations[t1] * standardDeviations[t2]);
                }
            }

            return correlationMatrix;
        };

        var getCorrelationGraph = function (correlationMatrix, cutoff) {
            var graph = { "nodes": [], "links": [] };
            for (var topic = 0; topic < numTopics; topic++) {
                graph.nodes.push({ "name": topic, "group": 1, "words": topNWords(topicWordCounts[topic], 3) });
            }
            for (var t1 = 0; t1 < numTopics; t1++) {
                for (var t2 = 0; t2 < numTopics; t2++) {
                    if (t1 !== t2 && correlationMatrix[t1][t2] > cutoff) {
                        graph.links.push({ "source": t1, "target": t2, "value": correlationMatrix[t1][t2] });
                    }
                }
            }
            return graph;
        };

        var plotGraph = function () {
            var correlationMatrix = getTopicCorrelations();
            var correlationGraph = getCorrelationGraph(correlationMatrix, correlationCutoff);

            var force = d3.layout.force()
                .charge(-220)
                .linkDistance(linkDistance)
                .nodes(correlationGraph.nodes)
                .links(correlationGraph.links)
                .size([w, h])
                .start();

            var link = vis.selectAll("line.link")
                .data(correlationGraph.links);

            link.enter().append("svg:line")
                .attr("class", "link")
                .style("stroke-width", "1px")
                .style("stroke", "#777");

            link.exit().remove();

            link.attr("x1", function (d) { return d.source.x; })
                .attr("y1", function (d) { return d.source.y; })
                .attr("x2", function (d) { return d.target.x; })
                .attr("y2", function (d) { return d.target.y; });

            var node = vis.selectAll("circle.node")
                .data(correlationGraph.nodes);
            node.enter().append("svg:circle")
                .attr("class", "node")
                .attr("r", 5)
                .style("fill", function (d) { return fill(d.group); });
            node.attr("cx", function (d) { return d.x; })
                .attr("cy", function (d) { return d.y; })
                .call(force.drag);

            var labels = vis.selectAll("text.node")
                .data(correlationGraph.nodes);

            labels.enter().append("svg:text")
                .attr("class", "node");

            labels.attr("x", function (d) { return d.x + 7; })
                .attr("y", function (d) { return d.y; })
                .text(function (d) { return d.words; })
                .call(force.drag);

            force.on("tick", function () {
                link.attr("x1", function (d) { return d.source.x; })
                    .attr("y1", function (d) { return d.source.y; })
                    .attr("x2", function (d) { return d.target.x; })
                    .attr("y2", function (d) { return d.target.y; });

                node.attr("cx", function (d) { return d.x; })
                    .attr("cy", function (d) { return d.y; });
                labels.attr("x", function (d) { return d.x + 7; })
                    .attr("y", function (d) { return d.y; });
            });
        };

        var toggleTopicDocuments = function (topic) {
            if (topic === selectedTopic) {
                // unselect the topic
                d3.selectAll("div.topicwords").attr("class", "topicwords");
                selectedTopic = -1;
            }
            else {
                d3.selectAll("div.topicwords").attr("class", function (d, i) { return i === topic ? "topicwords selected" : "topicwords"; });
                selectedTopic = topic;
            }
            reorderDocuments();
        };

        var mostFrequentWords = function () {
            // Convert the random-access map to a list of word:count pairs that
            //  we can then sort.
            var wordCounts = [];
            for (var word in vocabularyCounts) {
                wordCounts.push({ "word": word, "count": vocabularyCounts[word] });
            }

            wordCounts.sort(byCountDescending);
            return wordCounts;
        };

        var vocabTable = function () {
            var wordFrequencies = mostFrequentWords().slice(0, 499);
            var rows = d3.select("#vocab-table tbody").selectAll("tr")
               .data(wordFrequencies)
               .enter().append("tr");
            var cells = rows.selectAll("td")
               .data(function (row) { return [{ column: "word", value: row.word }, { column: "count", value: row.count }]; })
               .enter().append("td").text(function (d) { return d.value; });
        };

        /* Declare functions for various tabs and buttons */
        d3.select("#docs-tab").on("click", function () {
            d3.selectAll(".page").style("display", "none");
            d3.selectAll("ul li").attr("class", "");
            d3.select("#docs-page").style("display", "block");
            d3.select("#docs-tab").attr("class", "selected");
        });
        d3.select("#vocab-tab").on("click", function () {
            d3.selectAll(".page").style("display", "none");
            d3.selectAll("ul li").attr("class", "");
            d3.select("#vocab-page").style("display", "block");
            d3.select("#vocab-tab").attr("class", "selected");
        });
        d3.select("#corr-tab").on("click", function () {
            d3.selectAll(".page").style("display", "none");
            d3.selectAll("ul li").attr("class", "");
            d3.select("#corr-page").style("display", "block");
            d3.select("#corr-tab").attr("class", "selected");
        });
        d3.select("#sweep").on("click", function () {
            for (var s = 0; s < 50; s++) { sweep(); }
            reorderDocuments();
            sortTopicWords();
            displayTopicWords();
            plotGraph();
        });
        d3.select("#corr-slider")
          .on("change", function () {
              correlationCutoff = this.value / 100;
              plotGraph();
          });

        queue()
          .defer(d3.text, stopwordsURL)
          .defer(d3.text, documentsURL)
          .await(ready);

        function ready(error, stops, lines) {
            if (error) { alert("One of these URLs didn't work:\n " + stopwordsURL + "\n " + documentsURL); }
            else {
                // Create the stoplist
                stops.split("\n").forEach(function (w) { stopwords[w] = 1; });

                // Load documents and populate the vocabulary
                lines.split("\n").forEach(parseLine);

                sortTopicWords();
                displayTopicWords();
                plotGraph();

                vocabTable();
            }
        }

</script>
</body>
</html>
