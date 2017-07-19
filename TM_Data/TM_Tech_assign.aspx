<%@ Page Language="C#" MasterPageFile="~/Masters/RightCotentMaster.master" AutoEventWireup="true"
    CodeBehind="TM_Tech_assign.aspx.cs" Inherits="ZCZJ_DPF.TM_Data.TM_Tech_assign" %>

<%@ Register Src="../Controls/UCPaging.ascx" TagName="UCPaging" TagPrefix="uc1" %>
<%@ Register Src="../Controls/UserDefinedQueryConditions.ascx" TagName="UserDefinedQueryConditions"
    TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/JSRegister.ascx" TagName="JSRegister" TagPrefix="JSR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    技术任务表
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="PrimaryContent" runat="server">
    <JSR:JSRegister ID="JSRegister1" runat="server" />

    <script type="text/javascript">

        $(function() {
            $("#<%=hplExpotMS.ClientID %>").click(function() {
                var $input = $("#<%=GridView1.ClientID %> input[type=checkbox]:checked");
                if ($input.length != 1) { alert("请勾选单条任务号导出"); return false; }
                var taskId = $($input.parent().parent().parent().find("td")[2]).text();
                var date = new Date();
                var nouse = date.getDate();
                var obj = new Object();
                window.showModalDialog("TM_MS_ExprotExcel.aspx?action=" + taskId + "&time=" + nouse, obj, "dialogHeight:250px;dialogWidth:450px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");

            });
        });

        $(function() {
            $("#<%=hplExpotMP.ClientID %>").click(function() {
                var $input = $("#<%=GridView1.ClientID %> input[type=checkbox]:checked");
                if ($input.length != 1) { alert("请勾选单条任务号导出"); return false; }
                var taskId = $($input.parent().parent().parent().find("td")[2]).text();
                var date = new Date();
                var nouse = date.getDate();
                var obj = new Object();
                window.showModalDialog("TM_MP_ExportExcel.aspx?action=" + taskId + "&time=" + nouse, obj, "dialogHeight:250px;dialogWidth:450px;status:no;scroll:yes;center:yes;toolbar=no;menubar=no");

            });
        });


        //虚拟任务初始化弹窗
        $(function() {
            $("#win").show().dialog({
                title: '新增虚拟任务',
                width: 280,
                height: 200,
                closed: true,
                cache: false,
                modal: true,
                buttons: '#buttons'
            });
        });
        //打开虚拟任务弹窗
        function showVisualTask() {
            $("#win").dialog("open");

        }
        function saveVisualTask() {
            var taskId = $("#txtTaskId").val();
            var shebei = $("#txtShebei").val();
            if (taskId != "" && shebei != "") {
                $.ajax({
                    type: "POST",
                    url: "TM_AjaxHandler.aspx",
                    data: { "taskId": taskId, "shebei": shebei, "method": "AddVisualTask" },
                    success: function(msg) {
                      if (msg == "true") {
                            alert("添加成功！");
                        } else if(msg=="false"){
                            alert("添加失败，请勿插入重复任务");

                        }else{
                           alert("添加失败，任务号不能包含'.'");   
                        }

                        $("#txtTaskId").val("");
                        $("#txtShebei").val("");
                        $("#win").dialog("close");
                    }
                });
            }
        }
        
        //工装任务初始化弹窗
        $(function() {
            $("#wingz").show().dialog({
                title: '新增工装任务',
                width: 280,
                height: 200,
                closed: true,
                cache: false,
                modal: true,
                buttons: '#divgongz'
            });
        });
        //打开工装任务弹窗
        function showgongzTask() {
            $("#wingz").dialog("open");

        }
        function savegongzTask() {
            var taskIdgz = $("#txtGZTSAID").val();
            var shebeigz = $("#txtengname").val();
            if (taskIdgz != "" && shebeigz != "") {
                $.ajax({
                    type: "POST",
                    url: "TM_AjaxHandler.aspx",
                    data: { "taskIdgz": taskIdgz, "shebeigz": shebeigz, "method": "AddgongzTask" },
                    success: function(msg) {
                         if (msg == "true") {
                            alert("添加成功！");
                        } else if(msg=="false"){
                            alert("添加失败，请勿插入重复任务");

                        }else{
                           alert("添加失败，任务号不能包含'.'");   
                        }

                        $("#txtGZTSAID").val("");
                        $("#txtengname").val("");
                        $("#wingz").dialog("close");
                    }
                });
            }
        }


        function OnTxtPersonInfoKeyDown() {
            var dep = document.getElementById('<%=ddlSearch.ClientID%>');
            var acNameClientId = "<%=aceSearch.ClientID %>";
            var acName = $find(acNameClientId);
            if (acName != null) {
                acName.set_contextKey(dep.options[dep.selectedIndex].value);
            }
        }
        /*原始数据 选择一类数据*/
        function selectOrgAll(obj) {
            var table = document.getElementById('<%= GridView1.ClientID %>');
            if (obj.checked) {
                for (i = 1; i < table.rows.length; i++) {
                    objstr = table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    objstr.checked = true;
                }
            }
            else {
                for (i = 1; i < table.rows.length; i++) {
                    objstr = table.rows[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                    objstr.checked = false;
                }
            }
        }

    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <asp:Label ID="ControlFinder" runat="server" Visible="false"></asp:Label>
            <div class="box-title">
                <table style="width: 100%; height: 24px">
                    <tr>
                        <td align="center">
                            全选/取消<input id="Checkbox2" runat="server" onclick="selectOrgAll(this);" type="checkbox" />&nbsp;&nbsp;
                            <asp:Button ID="btnSelect" ToolTip="选择起止项后点击连选,起止项间所有记录选中" runat="server" OnClick="btnSelect_Click"
                                Text="连选" />
                        </td>
                        <td align="right">
                            按类查询:<asp:DropDownList ID="ddlSearch" runat="server" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                <asp:ListItem Text="-请选择-" Value=""></asp:ListItem>
                                <asp:ListItem Text="项目名称" Value="CM_PROJ"></asp:ListItem>
                                <asp:ListItem Text="设备名称" Value="TSA_ENGNAME"></asp:ListItem>
                                <asp:ListItem Text="任务号" Value="TSA_ID"></asp:ListItem>
                                <asp:ListItem Text="合同号" Value="TSA_PJID"></asp:ListItem>
                                <asp:ListItem Text="技术员" Value="TSA_TCCLERKNM"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSearch" runat="server" onkeydown="return OnTxtPersonInfoKeyDown()"
                                Width="120px"></asp:TextBox>
                            <cc1:AutoCompleteExtender ID="aceSearch" runat="server" CompletionSetCount="15" ServiceMethod="GetSuggestions_Tech"
                                FirstRowSelected="true" Enabled="true" DelimiterCharacters="" ServicePath="~/TM_Data/TM_WebService.asmx"
                                TargetControlID="txtSearch" UseContextKey="True" CompletionInterval="10" MinimumPrefixLength="1">
                            </cc1:AutoCompleteExtender>
                            <asp:Button ID="btnSearch" runat="server" Text=" 查 询 " OnClick="btnSearch_Click" />
                        </td>
                        <td align="right">
                            任务状态:
                        </td>
                        <td>
                            <asp:DropDownList ID="rblstatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                                <asp:ListItem Text="待分工" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="进行中" Value="1"></asp:ListItem>
                                <asp:ListItem Text="完工" Value="2"></asp:ListItem>
                                <asp:ListItem Text="停工" Value="3"></asp:ListItem>
                                <asp:ListItem Text="虚拟任务" Value="4"></asp:ListItem>
                                <asp:ListItem Text="工装任务" Value="6"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            排序:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSort" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblstatus_SelectedIndexChanged">
                                <asp:ListItem Text="任务号" Value="TSA_ID"></asp:ListItem>
                                <asp:ListItem Text="合同号" Value="TSA_PJID"></asp:ListItem>
                                <%--  <asp:ListItem Text="登记日期" Value="TSA_ADDTIME"></asp:ListItem>--%>
                                <asp:ListItem Text="技术员" Value="TSA_TCCLERKNM"></asp:ListItem>
                                 <asp:ListItem Text="开始日期" Selected="True" Value="TSA_STARTDATE"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        
                        <td align="center">
                            <asp:HyperLink ID="hplExpotMS" CssClass="hand" runat="server">
                                <asp:Image ID="AddImage" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />导出制作明细</asp:HyperLink>
                        </td>
                        <td align="center">
                            <asp:HyperLink ID="hplExpotMP" CssClass="hand" onclick="exportMSExcel();" runat="server">
                                <asp:Image ID="Image2" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />导出材料计划</asp:HyperLink>
                        </td>
                        <td align="center">
                            <asp:LinkButton ID="btnExpotOrg" CssClass="hand" runat="server" OnClick="btnExpotOrg_Click">
                                <asp:Image ID="Image3" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />导出原始数据</asp:LinkButton>
                        </td>
                        <td visible="false">
                            <asp:HyperLink ID="HyperLink1" CssClass="hand" runat="server">
                                <asp:Image ID="Image4" ImageUrl="~/Assets/icon-fuction/388.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />其它筛选</asp:HyperLink>
                            <cc1:PopupControlExtender ID="PopupControlExtender1" CacheDynamicResults="false"
                                Position="Left" Enabled="true" runat="server" OffsetX="-400" OffsetY="-25" TargetControlID="HyperLink1"
                                PopupControlID="palORG">
                            </cc1:PopupControlExtender>
                            <asp:Panel ID="palORG" Width="50%" Style="display: none; visibility: hidden; border-style: solid;
                                border-width: 1px; border-color: blue; background-color: Menu;" runat="server">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <div style="font-family: Verdana, Helvetica, Arial, sans-serif; line-height: 17px;
                                                        font-size: 11px; font-weight: bold; position: absolute; top: 8px; right: 10px;">
                                                        <a onclick="document.body.click(); return false;" style="background-color: #6699CC;
                                                            cursor: pointer; color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;"
                                                            title="关闭">X</a>
                                                    </div>
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <asp:Button ID="Button1" runat="server" UseSubmitBehavior="false" OnClick="btnSearch_Click"
                                                        Text="查 询" />
                                                    &nbsp;&nbsp;<asp:Button ID="Button2" runat="server" UseSubmitBehavior="false" OnClick="btnMSClear_OnClick"
                                                        Text="清 空" />&nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" style="width: 98%">
                                                    <uc3:UserDefinedQueryConditions runat="server" ID="udqMS" QueryColumnsType="TaskidView"
                                                        QueryRows="4" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="13" align="right">
                            <asp:LinkButton ID="lbnPlentyAssign" runat="server" OnClick="lbnPlentyAssign_Click">
                                <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                批量分工</asp:LinkButton>
                            <asp:HyperLink ID="lbnAddVisualTask" runat="server" onClick="showVisualTask()">
                                <asp:Image ID="Image1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                增加虚拟任务
                            </asp:HyperLink>
                            <asp:HyperLink ID="Hypgztsaid" runat="server" onClick="showgongzTask()">
                                <asp:Image ID="Image5" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                增加工装任务
                            </asp:HyperLink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer" style="width: 99%; overflow: auto;">
           <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridView1" Width="100%" CssClass="toptable grid" runat="server"
                        OnRowDataBound="GridView1_OnRowDataBound" AutoGenerateColumns="False" CellPadding="4"
                        ForeColor="#333333">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#1E5C95" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="10px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Width="8px" CssClass="checkBoxCss" />
                                </ItemTemplate>
                                <ItemStyle Width="8px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="序号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblIndex" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex +1) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="TSA_ID" HeaderText="任务号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TSA_PJID" HeaderText="合同号" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="CM_PROJ" HeaderText="项目名称" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TSA_ENGNAME" HeaderText="设备名称" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TSA_CONTYPE" HeaderText="设备类型" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TSA_TCCLERKNM" HeaderText="技术员" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TSA_REVIEWER" HeaderText="评审人" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TSA_STARTDATE" HeaderText="任务开始时间" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="TSA_MANCLERKNAME" HeaderText="任务分工人" HeaderStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="状态" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:Label ID="tsa_status" runat="server" Text='<%# Eval("TSA_STATE").ToString()=="0"?"待分工":Eval("TSA_STATE").ToString()=="1"?"进行中...":Eval("TSA_STATE").ToString()=="2"?"完工":Eval("TSA_STATE").ToString()=="4"?"虚拟任务":Eval("TSA_STATE").ToString()=="6"?"工装任务":"停工" %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="经营计划单" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlPJinfo" CssClass="link" runat="server" NavigateUrl='<%#"~/CM_Data/CM_TaskPinS.aspx?action=look&id="+Eval("ID") %>' Target="_blank">
                                        <asp:Image ID="AddImage3" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        查看单据
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="分工信息" ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false"
                                ItemStyle-Wrap="false">
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlTask" CssClass="link" runat="server" NavigateUrl='<%#"TM_Tech_Assign_Detail.aspx?tmdetail_id="+Eval("TSA_ID")%>'>
                                        <asp:Image ID="AddImage1" ImageUrl="~/Assets/images/create.gif" border="0" hspace="2"
                                            align="absmiddle" runat="server" />
                                        修改
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#B9D3EE" Font-Bold="True" ForeColor="#1E5C95" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    <asp:Panel ID="NoDataPanel" runat="server">
                        没有记录!</asp:Panel>
                    <uc1:UCPaging ID="UCPaging1" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rblstatus" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlSort" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnSelect" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="win" style="display: none">
        <form method="post" id="formVisualTask">
        <div>
            <table>
                <tr>
                    <td>
                        <strong>合同号</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <input name="txtContract" type="text" value="JSB.BOM001" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>任务号(图号)</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <input id="txtTaskId" type="text" class="easyui-validatebox" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>设备名称</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <input id="txtShebei" type="text" class="easyui-validatebox" data-options="required:true" />
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>
    <div id="buttons" style="text-align: right; display: none">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="saveVisualTask()">
            保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#win').dialog('close')">取消</a>
    </div>
    
    
    <div id="wingz" style="display: none">
        <form method="post" id="formgztask">
        <div>
            <table>
                <tr>
                    <td>
                        <strong>合同号</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <input name="txtGZHTBH" type="text" value="GONGZHUANG" readonly="readonly" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>任务号(图号)</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <input id="txtGZTSAID" type="text" class="easyui-validatebox" data-options="required:true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <strong>设备名称</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td>
                        <input id="txtengname" type="text" class="easyui-validatebox" data-options="required:true" />
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>
    <div id="divgongz" style="text-align: right; display: none">
        <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savegongzTask()">
            保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                onclick="javascript:$('#wingz').dialog('close')">取消</a>
    </div>
</asp:Content>
