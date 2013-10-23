
var redraw, g, renderer;

/* only do all this when document has finished loading (needed for RaphaelJS) */
window.onload = function () {
                
    var ts = Math.round(new Date().getTime() / 1000);       

    $.ajaxSetup({
        scriptCharset: "utf-8",
        contentType: "application/json; charset=utf-8"
    });

    $.ajax({
        type: "GET",
        url: "../HttpHandler/MessageHandler.ashx?operation=NodeGraph&RequestData= &ts=" + ts,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{id: '1'}",
        success: function (objNodes) {
            GraphRenderer(objNodes);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseText);
        }
    });


    
    //    console.log(g.nodes["kiwi"]);
};

var GraphRenderer = function (nodes) {

    var width = $(document).width() - 20;
    var height = $(document).height() - 60;

    g = new Graph();

    var hRender = function (r, n) {
        var label = r.text(10, 10, n.label);

        //the Raphael set is obligatory, containing all you want to display 
        var set = r.set().push(
            r.ellipse(10, 10, 30, 20)
             .attr({
                 "fill": "#fa8",
                 "stroke-width": "2px"
             }))
          .push(label);
        return set;
    };

    var vRender = function (r, n) {
        var label = r.text(10, 10, n.label);

        //the Raphael set is obligatory, containing all you want to display 
        var set = r.set().push(
            r.ellipse(10, 10, 30, 20)
             .attr({
                 "fill": "#bfac00",
                 "stroke-width": "2px"
             }))
          .push(label);
        return set;
    };

    var oRender = function (r, n) {
        var label = r.text(10, 10, n.label);

        //the Raphael set is obligatory, containing all you want to display 
        var set = r.set().push(
            r.ellipse(10, 10, 30, 20)
             .attr({
                 "fill": "#bf5600",
                 "stroke-width": "2px"
             }))
          .push(label);
        return set;
    };

    var gRender = function (r, n) {
        var label = r.text(10, 10, n.label);

        //the Raphael set is obligatory, containing all you want to display 
        var set = r.set().push(
            r.ellipse(10, 10, 30, 20)
             .attr({
                 "fill": "#7cbf00",
                 "stroke-width": "2px"
             }))
          .push(label);
        return set;
    };



    var camRender = function (r, n) {
        /* the Raphael set is obligatory, containing all you want to display */
        var set = r.set().push(
            /* custom objects go here */
            r.rect(n.point[0] - 30, n.point[1] - 13, 60, 44).attr({ "fill": "#feb", r: "12px", "stroke-width": "1px" })).push(
            r.text(n.point[0], n.point[1] + 10, (n.label || n.id)));
        return set;
    };

    $.each(nodes, function (i, node) {
        var blnBreak = true;
        var sourceId = node.name;
        var nodelLabel = node.name;
        g.addNode(sourceId, { label: nodelLabel, render: camRender });
        if (node.hasOwnProperty("children")) {
            $.each(node.children, function (j, firstGen) {
                nodeLabel = firstGen.name;
                var nodeId = sourceId + nodeLabel;
                var getRenderer = function (elm) {
                    if (elm.match(/H/))
                        return hRender;
                    else if (elm.match(/V/))
                        return vRender;
                    else if (elm.match(/G/))
                        return gRender;
                    else if (elm.match(/O/))
                        return oRender;
                    else return camRender;
                };
                g.addNode(nodeId, { label: nodeLabel, render: getRenderer(nodeLabel) });
                g.addEdge(sourceId, nodeId, { directed: true });
                if (firstGen.hasOwnProperty("children")) {
                    if (blnBreak) {
                        $.each(firstGen.children, function (k, secondGen) {
                            var secondGenLabel = secondGen.name;
                            var secondGenId = sourceId + secondGenLabel;
                            g.addNode(secondGenId, { label: secondGenLabel, render: getRenderer(nodeLabel) });
                            g.addEdge(nodeId, secondGenId, { directed: true });
                        });
                        blnBreak = false;
                    }
                }
            });
        }
    })

    ///* add a node for CAM1 */
    //g.addNode("CAM1", { label: "CAM1", render: camRender });
    //g.addNode("CAM1H1", { label: "H1", render: hRender });
    //g.addNode("CAM1H2", { label: "H2", render: hRender });
    //g.addNode("CAM1H3", { label: "H3", render: hRender });
    //g.addNode("CAM1H4", { label: "H4", render: hRender });
    //g.addNode("CAM1V1", { label: "V1", render: vRender });
    //g.addNode("CAM1V2", { label: "V2", render: vRender });
    //g.addNode("CAM1G1", { label: "G1", render: gRender });
    //g.addNode("CAM1G2", { label: "G2", render: gRender });

    ///* add a node for CAM1 */
    //g.addNode("CAM5", { label: "CAM5", render: camRender });
    //g.addNode("CAM5H1", { label: "H1", render: hRender });
    //g.addNode("CAM5H2", { label: "H2", render: hRender });
    //g.addNode("CAM5H3", { label: "H3", render: hRender });
    //g.addNode("CAM5H4", { label: "H4", render: hRender });
    //g.addNode("CAM5V1", { label: "V1", render: vRender });
    //g.addNode("CAM5V2", { label: "V2", render: vRender });
    //g.addNode("CAM5G1", { label: "G1", render: gRender });
    //g.addNode("CAM5G2", { label: "G2", render: gRender });

    ///* connect nodes with edges */
    //g.addEdge("CAM1", "CAM1H1", { directed: true });
    //g.addEdge("CAM1", "CAM1H2", { directed: true });
    //g.addEdge("CAM1", "CAM1H3", { directed: true });
    //g.addEdge("CAM1", "CAM1H4", { directed: true });
    //g.addEdge("CAM1H1", "CAM1V1", { directed: true });
    //g.addEdge("CAM1H1", "CAM1V2", { directed: true });
    //g.addEdge("CAM1H2", "CAM1V1", { directed: true });
    //g.addEdge("CAM1H2", "CAM1V2", { directed: true });
    //g.addEdge("CAM1H3", "CAM1V1", { directed: true });
    //g.addEdge("CAM1H3", "CAM1V2", { directed: true });
    //g.addEdge("CAM1H4", "CAM1V1", { directed: true });
    //g.addEdge("CAM1H4", "CAM1V2", { directed: true });
    //g.addEdge("CAM1H1", "CAM1G1", { directed: true });
    //g.addEdge("CAM1H1", "CAM1G2", { directed: true });

    ///* connect nodes with edges */
    //g.addEdge("CAM5", "CAM5H1", { directed: true });
    //g.addEdge("CAM5", "CAM5H2", { directed: true });
    //g.addEdge("CAM5", "CAM5H3", { directed: true });
    //g.addEdge("CAM5", "CAM5H4", { directed: true });
    //g.addEdge("CAM5H1", "CAM5V1", { directed: true });
    //g.addEdge("CAM5H1", "CAM5V2", { directed: true });
    //g.addEdge("CAM5H2", "CAM5V1", { directed: true });
    //g.addEdge("CAM5H2", "CAM5V2", { directed: true });
    //g.addEdge("CAM5H3", "CAM5V1", { directed: true });
    //g.addEdge("CAM5H3", "CAM5V2", { directed: true });
    //g.addEdge("CAM5H4", "CAM5V1", { directed: true });
    //g.addEdge("CAM5H4", "CAM5V2", { directed: true });
    //g.addEdge("CAM5H1", "CAM5G1", { directed: true });
    //g.addEdge("CAM5H1", "CAM5G2", { directed: true });

    //g.addEdge("CAM1H1", "CAM5H1", { directed: true });
    //g.addEdge("CAM1H2", "CAM5H2", { directed: true });
    //g.addEdge("CAM1H3", "CAM5H3", { directed: true });
    //g.addEdge("CAM1H4", "CAM5H4", { directed: true });

    /* layout the graph using the Spring layout implementation */
    var layouter = new Graph.Layout.Spring(g);

    /* draw the graph using the RaphaelJS draw implementation */
    renderer = new Graph.Renderer.Raphael('canvas', g, width, height);

    //redraw = function () {
    //    layouter.layout();
    //    renderer.draw();
    //};
    //hide = function (id) {
    //    g.nodes[id].hide();
    //};
    //show = function (id) {
    //    g.nodes[id].show();
    //};

};

