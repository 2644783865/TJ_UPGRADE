<%@ Page Language="C#" MasterPageFile="~/Masters/SMBaseMaster.Master" AutoEventWireup="true"
    CodeBehind="SM_PURCHASEPLAN_VIEW.aspx.cs" Inherits="ZCZJ_DPF.SM_Data.SM_PURCHASEPLAN_VIEW"
    Title="需要计划查询" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Assembly="YYControls" Namespace="YYControls" TagPrefix="yyc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">

    <script src="SM_JS/SelectCondition.js" type="text/javascript"></script>

    <link href="jmodel/css/jquery.jmodal.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .accordion
        {
            width: 100%;
        }
        .accordionHeader
        {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: #2E4d7B;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }
        .accordionHeaderSelected
        {
            border: 1px solid #2F4F4F;
            color: white;
            background-color: #5078B3;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }
        .accordionContent
        {
            background-color: #D3DEEF;
            border: 1px dashed #2F4F4F;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
        }
    </style>

    <script src="../SM_Data/jmodel/js/jquery-1.3.1.min.js" type="text/javascript"></script>

    <script src="../SM_Data/jmodel/js/jquery.jmodal.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
    
  
//    function ShowChangeModal(ptc) {
//    
//        var retVal = window.showModalDialog("SM_PURCHASEPLAN_CHANGE.aspx?ptc="+ptc, "", "dialogWidth=1000px;dialogHeight=500px;help=no;scroll=yes");

//    }
     function ShowChangeModal(ptc) {
                $.fn.jmodal({
                    data: { innerText:'Information' },
                    
                    title: '变更信息 ',
                    content: function(body) {
                        body.html('加载中...');
                        body.load('SM_PURCHASEPLAN_CHANGE.aspx?ptc='+ptc+'&&temp='+Math.round(Math.random() * 10000));
                    },
                    width:1000,
                    height:200,
                    fixed: false,
                    buttonText: { ok: 'Yes,It is.', cancel: 'No'},
                    okEvent: function(data, args) {
                    	 alert(data.innerText);
                    }
                });
            }
    
    
//     function ShowReplaceModal(ptc) {
//    
//        var retVal = window.showModalDialog("SM_PURCHASEPLAN_REPLACE.aspx?ptc="+ptc, "", "dialogWidth=1000px;dialogHeight=500px;help=no;scroll=yes");

//    }
    function ShowReplaceModal(ptc) {
                $.fn.jmodal({
                    data: { innerText:'Information' },
                   
                    title: '代用信息 ',
                    content: function(body) {
                        body.html('加载中...');
                        body.load('SM_PURCHASEPLAN_REPLACE.aspx?ptc='+ptc+'&&temp='+Math.round(Math.random() * 10000));
                    },
                    width:1000,
                    height:200,
                    fixed: false,
                    buttonText: { ok: 'Yes,It is.', cancel: 'No'},
                    okEvent: function(data, args) {
                    	 alert(data.innerText);
                    }
                });
            }
    
    
     function ShowPLan(planno,shape) {
    
      
        window.open("../PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?shape="+escape(shape)+"&mp_id="+escape(planno));
    }
    function ShowStoUseModal(ptc) {
                $.fn.jmodal({
                    data: { innerText:'Information' },
                    
                    title: '占用信息 ',
                    content: function(body) {
                        body.html('加载中...');
                        body.load('SM_PURCHASEPLAN_STOUSE.aspx?ptc='+ptc+'&&temp='+Math.round(Math.random() * 10000));
                    },
                    width:1000,
                    height:200,
                    fixed: false,
                    buttonText: { ok: 'Yes,It is.', cancel: 'No'},
                    okEvent: function(data, args) {
                    	 alert(data.innerText);
                    }
                });
            }
            
          function ShowINModal(ptc){
            window.open("SM_WarehouseIn_Manage.aspx?FLAG=XRIN&&PTC="+ptc);
          }
            
          function ShowOutModal(ptc){
            window.open("SM_WarehouseOUT_LL_Manage.aspx?FLAG=XROUT&&PTC="+ptc);
          }
          
    
    
      //计算遮罩层的高
    function getHeight(){
        var winHeight
        if (document.documentElement.scrollHeight > document.documentElement.clientHeight) {
            winHeight = document.documentElement.scrollHeight;
        }
        else {
            winHeight = document.documentElement.clientHeight;
        }
        if (navigator.appName !== "Microsoft Internet Explorer") {
            winHeight = winHeight - 4;
        }
        else {
            winHeight = winHeight;
        }
        return winHeight;
    }
    
    //计算遮罩层的宽
    function getWidth(){
        var winWidth;
        if (document.documentElement.scrollWidth > document.documentElement.clientWidth) {
            winWidth = document.documentElement.scrollWidth;
        }
        else {
            winWidth = document.documentElement.clientWidth;
        }
        if (navigator.appName !== "Microsoft Internet Explorer") {
            winWidth = winWidth - 4;
        }
        else {
            winWidth = winWidth;
        }
        return winWidth;
    }

    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="True">
    </cc1:ToolkitScriptManager>
    <asp:Panel ID="PanelSearch" runat="server">
        <table width="100%">
            <tr>
                <td style="white-space: nowrap; width: 60%;" align="left">
                    <asp:CheckBox ID="CheckBoxReplace" runat="server" CssClass="checkBoxCss" Text="代用"
                        TextAlign="Left" />
                </td>
                <td style="white-space: nowrap; width: 20%;" align="left">
                    &nbsp;
                </td>
                <td style="white-space: nowrap; width: 20%;" align="right">
                    <asp:Button ID="QueryButton" runat="server" OnClick="QueryButton_Click" Text="查询" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnReset" runat="server" Text="重置" OnClick="btnReset_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnExport" runat="server" Text="导出" OnClick="btnExport_Click" />
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
    <cc1:Accordion ID="Accordion1" runat="server" CssClass="accordion" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        RequireOpenedPane="false">
        <Panes>
            <cc1:AccordionPane ID="AccordionPane1" runat="server">
                <Header>
                    单级筛选</Header>
                <Content>
                    <asp:Panel ID="PanelCondition" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td colspan="3">
                                    <font style="color: #FF0000;">1.采购数量为0,且字体颜色为红色,为订单关闭;而采购数量为0，字体颜色不变的，为未下推订单;
                                        <br />
                                        2.由于钢板不是按照计划跟踪号出库，故钢板只能查看计划数，出库数量不准确; </font>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    字段:&nbsp;<asp:DropDownList ID="DropDownListType" runat="server" OnSelectedIndexChanged="TypeOrOrderBy_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="sqrtime" Selected="True">计划时间</asp:ListItem>
                                        <asp:ListItem Value="pjnm">项目</asp:ListItem>
                                        <asp:ListItem Value="engnm">工程</asp:ListItem>
                                        <asp:ListItem Value="ptcode">计划跟踪号</asp:ListItem>
                                        <asp:ListItem Value="depnm">计划部门</asp:ListItem>
                                        <asp:ListItem Value="margg">规格</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    <asp:RadioButtonList ID="RadioButtonListOrderBy" runat="server" RepeatDirection="Horizontal"
                                        BorderStyle="None" OnSelectedIndexChanged="TypeOrOrderBy_SelectedIndexChanged"
                                        AutoPostBack="True">
                                        <asp:ListItem Value="1" Selected="True">降序</asp:ListItem>
                                        <asp:ListItem Value="0">升序</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td style="white-space: nowrap;" align="left">
                                    变&nbsp;&nbsp;更&nbsp;&nbsp;状&nbsp;&nbsp;态:&nbsp;<asp:DropDownList ID="DropDownListChange"
                                        runat="server">
                                        <asp:ListItem Value="">请选择</asp:ListItem>
                                        <asp:ListItem Value="正常">正常</asp:ListItem>
                                        <asp:ListItem Value="变更">变更</asp:ListItem>
                                    </asp:DropDownList>
                                    &nbsp;
                                </td>
                                <td style="white-space: nowrap;" align="left">
                                    占&nbsp;&nbsp;用&nbsp;&nbsp;状&nbsp;&nbsp;态:&nbsp;<asp:DropDownList ID="DropDownListStoUse"
                                        runat="server">
                                        <asp:ListItem Value="">请选择</asp:ListItem>
                                        <asp:ListItem Value="正常">正常</asp:ListItem>
                                        <asp:ListItem Value="占用">占用</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="white-space: nowrap;" align="left">
                                    相似占用状态:&nbsp;<asp:DropDownList ID="DropDownListXSReplace" runat="server">
                                        <asp:ListItem Value="">请选择</asp:ListItem>
                                        <asp:ListItem Value="正常">正常</asp:ListItem>
                                        <asp:ListItem Value="相似占用">相似占用</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="white-space: nowrap;" align="left">
                                    代用状态:&nbsp;
                                    <asp:DropDownList ID="DropDownListReplace" runat="server">
                                        <asp:ListItem Value="">请选择</asp:ListItem>
                                        <asp:ListItem Value="正常">正常</asp:ListItem>
                                        <asp:ListItem Value="代用">代用</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="white-space: nowrap;" align="left">
                                    订单代用状态:&nbsp;<asp:DropDownList ID="DropDownListDDReplace" runat="server">
                                        <asp:ListItem Value="">请选择</asp:ListItem>
                                        <asp:ListItem Value="正常">正常</asp:ListItem>
                                        <asp:ListItem Value="订单代用">订单代用</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    项&nbsp;&nbsp;目&nbsp;&nbsp;名&nbsp;&nbsp;称:&nbsp;<asp:TextBox ID="TextBoxPro" runat="server"></asp:TextBox>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    工&nbsp;&nbsp;程&nbsp;&nbsp;名&nbsp;&nbsp;称:&nbsp;<asp:TextBox ID="TextBoxEng" runat="server"></asp:TextBox>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    部门:&nbsp;<asp:TextBox ID="TextBoxDep" runat="server"></asp:TextBox>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    技术员:&nbsp;<asp:TextBox ID="TextBoxMan" runat="server"></asp:TextBox>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    计划跟踪号:<asp:TextBox ID="TextBoxPTC" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    物&nbsp;&nbsp;料&nbsp;&nbsp;代&nbsp;&nbsp;码:&nbsp;<asp:TextBox ID="TextBoxMarID" runat="server"></asp:TextBox>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    物&nbsp;&nbsp;料&nbsp;&nbsp;名&nbsp;&nbsp;称:&nbsp;<asp:TextBox ID="TextBoxMarNM" runat="server"></asp:TextBox>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    规格:&nbsp;<asp:TextBox ID="TextBoxGG" runat="server"></asp:TextBox>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    材&nbsp;&nbsp;&nbsp;质:&nbsp;<asp:TextBox ID="TextBoxCZ" runat="server"></asp:TextBox>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    物料类型:<asp:DropDownList ID="DropDownListMarType" runat="server">
                                        <asp:ListItem Value="">请选择</asp:ListItem>
                                        <asp:ListItem Value="标(组装)">标(组装)</asp:ListItem>
                                        <asp:ListItem Value="标(发运)">标(发运)</asp:ListItem>
                                        <asp:ListItem Value="定尺板">定尺板</asp:ListItem>
                                        <asp:ListItem Value="非定尺板">非定尺板</asp:ListItem>
                                        <asp:ListItem Value="型材">型材</asp:ListItem>
                                        <asp:ListItem Value="协A">协A</asp:ListItem>
                                        <asp:ListItem Value="协B">协B</asp:ListItem>
                                        <asp:ListItem Value="电气电料">电气电料</asp:ListItem>
                                        <asp:ListItem Value="油漆">油漆</asp:ListItem>
                                        <asp:ListItem Value="其他">其他</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    计划开始时间:&nbsp;<asp:TextBox ID="TextBoxStartDate" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="TextBoxStartDate_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                        TargetControlID="TextBoxStartDate">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    计划结束时间:&nbsp;<asp:TextBox ID="TextBoxEndTime" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="DateTextBox_CalendarExtender" runat="server" Format="yyyy-MM-dd"
                                        TargetControlID="TextBoxEndTime">
                                    </cc1:CalendarExtender>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    标识号:&nbsp;<asp:TextBox ID="TextBoxTUHAO" runat="server"></asp:TextBox>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    是否下推:&nbsp;<asp:DropDownList ID="DropDownListPushState" runat="server">
                                        <asp:ListItem Value="0" Selected="True">-请选择-</asp:ListItem>
                                        <asp:ListItem Value="1">未下推</asp:ListItem>
                                        <asp:ListItem Value="2">已下推</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td style="white-space: nowrap; width: 20%;" align="left">
                                    逻辑
                                    <asp:DropDownList ID="DropDownListFatherLogic" runat="server">
                                        <asp:ListItem Value="AND" Selected="True">并且</asp:ListItem>
                                        <asp:ListItem Value="OR">或者</asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </Content>
            </cc1:AccordionPane>
            <cc1:AccordionPane ID="AccordionPane2" runat="server">
                <Header>
                    多级筛选</Header>
                <Content>
                    <div style="width: 400px; margin-right: auto; margin-left: auto;">
                        <asp:GridView ID="GridViewSearch" runat="server" AutoGenerateColumns="False" CssClass="toptable grid"
                            CellPadding="3" Font-Size="9pt" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                            BorderWidth="1px" OnDataBound="GridViewSearch_DataBound" Width="100%">
                            <FooterStyle BackColor="White" ForeColor="#000066" />
                            <Columns>
                                <asp:TemplateField HeaderText="逻辑">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tb_logic" runat="server" Text='' onclick="infoc(this)" Width="60px"></asp:TextBox>
                                        <asp:DropDownList ID="DropDownListLogic" runat="server" Width="60px" Style="display: none">
                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                                            <asp:ListItem Value="OR" Selected="True">或者</asp:ListItem>
                                            <asp:ListItem Value="AND">并且</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="名称">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tb_name" runat="server" Text='' onclick="infoc(this)" Width="128px"></asp:TextBox>
                                        <asp:DropDownList ID="DropDownListName" runat="server" Width="128px" Style="display: none">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="130px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="比较关系">
                                    <ItemTemplate>
                                        <asp:TextBox ID="tb_relation" runat="server" Text='' onclick="infoc(this)" Width="80px"></asp:TextBox>
                                        <asp:DropDownList ID="DropDownListRelation" runat="server" Width="80px" Style="display: none">
                                            <%--<asp:ListItem Value="NO" Text=""></asp:ListItem>--%>
                                            <asp:ListItem Value="0" Selected="True">包含</asp:ListItem>
                                            <asp:ListItem Value="1">等于</asp:ListItem>
                                            <asp:ListItem Value="2">不等于</asp:ListItem>
                                            <asp:ListItem Value="3">大于</asp:ListItem>
                                            <asp:ListItem Value="4">大于或等于</asp:ListItem>
                                            <asp:ListItem Value="5">小于</asp:ListItem>
                                            <asp:ListItem Value="6">小于或等于</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="数值">
                                    <ItemTemplate>
                                        <asp:TextBox ID="TextBoxValue" runat="server" Width="128px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="140px" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                </Content>
            </cc1:AccordionPane>
        </Panes>
    </cc1:Accordion>
    <yyc:SmartGridView ID="GridView1" runat="server" EmptyDataText="无数据!" DataKeyNames="planno,ptcode,purstate,PUR_MASHAPE"
        AutoGenerateColumns="false" OnRowDataBound="GridView1_RowDataBound" ShowFooter="True">
        <Columns>
            <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                <ItemTemplate>
                    <%# Container.DataItemIndex + Convert.ToInt32(Eval("RowIndex"))%>
                </ItemTemplate>
            </asp:TemplateField>
         
            <asp:BoundField DataField="ptcode" HeaderText="计划跟踪号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="marid" HeaderText="物料编码" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="marnm" HeaderText="物料名称" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="margg" HeaderText="规格" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="marcz" HeaderText="材质" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="margb" HeaderText="国标" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PUR_TUHAO" HeaderText="标识号" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="length" HeaderText="长" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="width" HeaderText="宽" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="marunit" HeaderText="单位" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="num" HeaderText="计划数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="marfzunit" HeaderText="辅助单位" HeaderStyle-Wrap="false"
                ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="fznum" HeaderText="辅助数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:TemplateField HeaderText="采购数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelCKNUM" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="入库数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelINNUM" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="仓库" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="Labelwarehouse" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="仓位" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="Labelwarehouseposition" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="出库数量" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                ItemStyle-Wrap="false">
                <ItemTemplate>
                    <asp:Label ID="LabelOUTNUM" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="changestate" HeaderText="变更" HeaderStyle-Wrap="false"
                ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="stousestate" HeaderText="占用" HeaderStyle-Wrap="false"
                ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="xsreplacestate" HeaderText="相似占用" HeaderStyle-Wrap="false"
                ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="replacestate" HeaderText="代用" HeaderStyle-Wrap="false"
                ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="ddreplacestate" HeaderText="订单代用" HeaderStyle-Wrap="false"
                ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="marfzunit" HeaderText="辅助单位" HeaderStyle-Wrap="false"
                ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" Visible="false" />
            <asp:BoundField DataField="fznum" HeaderText="计划辅助数量" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" Visible="false" />
            <%-- <asp:TemplateField HeaderText="订单信息" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <asp:HyperLink ID="hplDD" CssClass="hand" runat="server">
                        <asp:Image ID="AddImage" ImageUrl="~/Assets/icons/dindan.jpeg" Height="16" Width="16"
                            border="0" hspace="2" align="absmiddle" runat="server" />
                    </asp:HyperLink>
                    <cc1:PopupControlExtender ID="PopupControlExtenderDD" runat="server" DynamicServiceMethod="GetDDInfo"
                        DynamicContextKey='<%# Eval("ptcode") %>' DynamicControlID="PanelDD" TargetControlID="hplDD"
                        PopupControlID="PanelDD" Position="Right" OffsetY="25">
                    </cc1:PopupControlExtender>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField DataField="pjnm" HeaderText="项目" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="engnm" HeaderText="工程" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PUR_MASHAPE" HeaderText="物料类型" HeaderStyle-Wrap="false"
                ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="depnm" HeaderText="部门" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="sqrnm" HeaderText="技术员" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="sqrtime" HeaderText="计划日期" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="PUR_ZYDY" HeaderText="行关闭备注" HeaderStyle-Wrap="false"
                ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" />
            <asp:BoundField DataField="purnote" HeaderText="备注" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"
                ItemStyle-HorizontalAlign="Center" />
        </Columns>
        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <FooterStyle BackColor="#EFF3FB" />
        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
        <FixRowColumn FixRowType="Header,Pager" TableHeight="380px" TableWidth="100%" FixColumns="0,1,2,3,4,5" />
    </yyc:SmartGridView>
    <asp:Panel ID="NoDataPanel" runat="server">
        没有任务!</asp:Panel>
    <uc1:UCPaging ID="UCPaging1" runat="server" />
    <asp:HiddenField ID="hfdnum" runat="server" />
    <asp:Panel ID="PanelDD" runat="server">
    </asp:Panel>

    <script type="text/javascript">
  
  
   var sDataTable=document.getElementById('<%=GridView1.ClientID%>')
  
   function RowClick()
   {
           for (var i=1, j=sDataTable.tBodies[0].rows.length-1; i<j; i++) 
          {
            sDataTable.tBodies[0].rows[i].onclick= function (i) 
            {
                var clicked = false;
                var dataRow = sDataTable.tBodies[0].rows[i];
                return function () 
                      {
                            if (clicked) 
                            {
                                dataRow.style.backgroundColor = "#EFF3FB";
                              
                                clicked = false;
                            }
                            else 
                            {
                                dataRow.style.backgroundColor = "#A8B7EC";
                                clicked = true;
                            }
                        }
             }.call(this, i);
           }
   }

   RowClick();

    </script>

</asp:Content>
