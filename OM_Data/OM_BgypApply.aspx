<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Masters/RightCotentMaster.Master"
    CodeBehind="OM_BgypApply.aspx.cs" Inherits="ZCZJ_DPF.OM_Data.OM_BgypApply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="RightContentTitlePlace" runat="server">
    新增办公用品申请单
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PrimaryContent" runat="server">
    <link href="../Assets/AutoCompleteTextBox.css" rel="stylesheet" type="text/css" />

    <script src="../JS/PickPersons.js" type="text/javascript"></script>

    <script type="text/javascript">
        var state;
        function allsel() {
            var table = document.getElementById("ctl00_PrimaryContent_GridView1");
            var tr = table.getElementsByTagName("tr");
            for (var i = 1; i < tr.length; i++) {
                if (tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0] != null) {
                    tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = true;
                }
            }
        }

        function cancelsel() {
            var table = document.getElementById("ctl00_PrimaryContent_GridView1");
            var tr = table.getElementsByTagName("tr");
            for (var i = 1; i < tr.length; i++) {
                if (tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0] != null) {
                    tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = false;
                    //tr[i].style.backgroundColor='#EFF3FB'; 
                }
            }
        }

        function consel() {
            table = document.getElementById("ctl00_PrimaryContent_GridView1");
            tr = table.getElementsByTagName("tr"); //这里的tr是一个数组
            for (var i = 1; i < (tr.length - 1); i++) {
                obj = tr[i].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                if (obj.type.toLowerCase() == "checkbox" && obj.value != "") {
                    if (obj.checked) {
                        obj.checked = true;
                        //                obj.parentNode.parentNode.style.backgroundColor ='#55DF55'; 
                        for (var j = i + 1; j < tr.length; j++) {
                            var nextobj = tr[j].getElementsByTagName("td")[0].getElementsByTagName("input")[0];
                            if (nextobj != null) {

                                if (nextobj.type.toLowerCase() == "checkbox" && nextobj.value != "") {
                                    if (nextobj.checked) {
                                        for (var k = i + 1; k < j + 1; k++) {
                                            tr[k].getElementsByTagName("td")[0].getElementsByTagName("input")[0].checked = true;
                                            //                                    tr[k].style.backgroundColor ='#55DF55'; 
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        var i;
        array = new Array();
        var $ddlDep;
        function SelTechPersons1() {
            $("#hidPerson").val("first");
            SelPersons();
        }

        //点击确定
        function savePick() {
            var r = Save();
            var id = $("#hidPerson").val();
            if (id == "first") {
                //            console.log(r.st_name);
                $("#<%=txtshr.ClientID %>").val(r.st_name);
                $("#<%=shrid.ClientID %>").val(r.st_id);
                console.log($("#<%=txtshr.ClientID %>").val());
            }
            $('#win').dialog('close');
        }

    </script>

    <script type="text/javascript">

        function autoCode(input) {
            var arrString = $(input).val();
            var $tr = $(input).parent().parent();
            if (arrString != null) {
                var array = arrString.split('|');
                //圆珠笔|蓝色|0.5|1-1-4-2|41
                $tr.find("input[name*=WLNAME]").val(array[0]);
                $tr.find("input[name*=WLMODEL]").val(array[1]);
                $tr.find("input[name*=WLPRICE]").val(array[2]);
                $tr.find("input[name*=WLUNIT]").val(array[3]);
                $tr.find("input[name*=WLBM]").val(array[4]);
                $tr.find("input[name*=WLCODE]").val(array[5]);
                $tr.find("input[name*=WLNUM]").val("1");
                $tr.find("input[name*=WLJE]").val(array[2]);
                $tr.find("input[name*=num]").val(array[6]);
                $tr.find("input[name*=unPrice]").val(array[7]);
            }
        }
        function sum(input) {
            var $tr = $(input).parent().parent();
            var dj = $tr.find("input[name*=WLPRICE]").val();
            var num = $tr.find("input[name*=WLNUM]").val();
            $tr.find("input[name*=WLJE]").val((dj * num).toFixed(2));
        }

        function sum_get(input) {
            var $tr = $(input).parent().parent();
            var dj = $tr.find("input[name*=unPrice]").val();
            var num = $tr.find("input[name*=WLSLS]").val();
            $tr.find("input[name*=GET_MONEY]").val((dj * num).toFixed(2));
        }
    
    </script>

    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <div class="box-inner">
        <div class="box_right">
            <div class="box-title">
            </div>
        </div>
    </div>
    <div class="box-wrapper">
        <div class="box-outer">
            <div style="margin: 0px 0px 0px 10px">
                <table width="99%" align="center" cellpadding="4" cellspacing="1" class="toptable grid">
                    <tr>
                        <td style="width: 50%" align="left">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 增加行:<asp:TextBox ID="txtNum" runat="server" Width="50px"
                                CssClass="center"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnadd" runat="server" Text="确 定" OnClick="btninsert_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btndelete" runat="server" Text="删除行" OnClick="btndelete_Click" OnClientClick="return alert('确定删除此条目吗？')" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnsave" runat="server" Text="保 存" OnClick="Save_Click" OnClientClick="return checkPage();" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="Close" Text="关闭" OnClick="close" runat="server" />&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Panel ID="HeadPanel" runat="server" Width="100%" Style="overflow: auto; position: static">
                <table width="100%">
                    <tr>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;编&nbsp;&nbsp;&nbsp;号：<asp:Label ID="LabelCode"
                                runat="server"></asp:Label>
                            <asp:Label ID="LabelState" runat="server" Visible="false"></asp:Label>
                            <input type="text" id="InputColour" style="display: none" runat="server" />
                        </td>
                        <td style="font-size: x-large; text-align: center;" colspan="2">
                            办公用品申请单
                            <asp:Image ID="ImageVerify" runat="server" ImageUrl="~/Assets/images/shenhe.gif"
                                Visible="false" />
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;日&nbsp;&nbsp;&nbsp;期：<asp:TextBox ID="TextBoxDate" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;制&nbsp;&nbsp;&nbsp;单&nbsp;&nbsp;&nbsp;人：<asp:Label ID="LabelDoc"
                                runat="server"></asp:Label>
                            <asp:Label ID="LabelDocCode" runat="server" Visible="false"></asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;审&nbsp;&nbsp;&nbsp;核&nbsp;&nbsp;&nbsp;人：
                            <asp:TextBox ID="txtshr" runat="server" Width="150px" onfocus="this.blur()"></asp:TextBox>
                            <input id="shrid" type="text" runat="server" readonly="readonly" style="display: none" />
                            <asp:HyperLink ID="hlSelect0" runat="server" CssClass="hand" OnClick="SelTechPersons1()">
                                <asp:Image ID="AddImage0" ImageUrl="~/Assets/images/h1.gif" border="0" hspace="2"
                                    align="absmiddle" runat="server" />
                                选择</asp:HyperLink>
                        </td>
                        <td>
                            <asp:DropDownList ID="drop_view" runat="server" Visible="false">
                                <asp:ListItem Text="同意" Value="2" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="不同意" Value="1"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <%-- <div>
                &nbsp;&nbsp;&nbsp;<input id="all" type="button" value="全选" onclick="allsel()" />&nbsp;&nbsp;&nbsp;<input
                    id="continue" type="button" value="连选" onclick="consel()" />
                &nbsp;&nbsp;&nbsp;<input id="cancelsel1" type="button" value="取消" onclick="cancelsel()" />
            </div>--%>
            <asp:Panel ID="PanelBody" runat="server" Style="height: 350px;">
                <div style="width: 100%; margin: 0 auto; overflow-x: auto">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" PageSize="20"
                        CellPadding="4" CssClass="toptable grid" ForeColor="#333333" Width="100%">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="15px">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Width="15px" CssClass="checkBoxCss" />
                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lbindex" runat="server" Text="<%# Convert.ToInt32(Container.DataItemIndex +1) %>"></asp:Label>
                                    <input type="hidden" id="hidStoreId" value='<%#Eval("StoreId") %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="编码">
                                <ItemTemplate>
                                    <input type="text" id="WLBM" style="width: 80px" runat="server" name="maId" readonly="readonly"
                                        value='<%#Eval("WLBM") %>' />
                                    <input id="WLCODE" runat="server" name="sId" type="hidden" value='<%#Eval("WLCODE") %>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="名称">
                                <ItemTemplate>
                                    <asp:TextBox ID="WLNAME" name="name" runat="server" Width="80px" onchange="autoCode(this)"
                                        Text='<%#Eval("WLNAME") %>'></asp:TextBox>
                                    <cc1:AutoCompleteExtender ID="marid_AutoCompleteExtender" runat="server" CompletionSetCount="15"
                                        CompletionListCssClass="autocomplete_completionListElement" CompletionListItemCssClass="autocomplete_listItem"
                                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" DelimiterCharacters=""
                                        Enabled="True" MinimumPrefixLength="1" ServiceMethod="HmCode" FirstRowSelected="true"
                                        ServicePath="~/OM_Data/Ajax.asmx" TargetControlID="WLNAME" UseContextKey="True"
                                        CompletionInterval="10">
                                    </cc1:AutoCompleteExtender>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="规格型号" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="WLMODEL" name="canshu" runat="server" style="border-style: none; width: 80px"
                                        type="text" value='<%#Eval("WLMODEL") %>' readonly="readonly" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单位" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="WLUNIT" name="unit" runat="server" style="border-style: none; width: 40px"
                                        type="text" value='<%#Eval("WLUNIT") %>' onkeyup="grControlFocus(this)" readonly="readonly" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="申请数量" ItemStyle-BackColor="#FFFFCC" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="WLNUM" name="num" runat="server" style="border-style: none; width: 80px"
                                        type="text" value='<%#Eval("WLNUM") %>' onkeyup="sum(this)" />
                                </ItemTemplate>
                                <ItemStyle BackColor="#FFFFCC" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单价" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="WLPRICE" name="dj" runat="server" style="border-style: none; width: 80px"
                                        type="text" value='<%#Eval("WLPRICE") %>' onkeyup="sum(this)" readonly="readonly" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="金额" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="WLJE" name="je" runat="server" style="border-style: none; width: 80px"
                                        type="text" value='<%#Eval("WLJE") %>' readonly="readonly" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否计数额度" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlIsCal" SelectedValue='<%#Eval("IsCalculate") %>'
                                        Enabled="false">
                                        <asp:ListItem Text="是" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="否" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="是否以旧换新" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList runat="server" ID="ddlIsChange" SelectedValue='<%#Eval("IsChange") %>'>
                                        <asp:ListItem Text="是" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="否" Value="0" Selected="True"></asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="实领数" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="WLSLS" name="sls" disabled="disabled" runat="server" style="border-style: none;
                                        width: 80px" type="text" value='<%#Eval("WLSLS") %>' onkeyup="sum_get(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="实领金额" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="GET_MONEY" name="get_money" disabled="disabled" runat="server" style="border-style: none;
                                        width: 80px" type="text" value='<%#Eval("GET_MONEY") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="及时库存数量" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="num" name="num" readonly="readonly" runat="server" style="border-style: none;
                                        width: 80px" type="text" value='<%#Eval("num") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="库存单价" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="unPrice" name="unPrice" readonly="readonly" runat="server" style="border-style: none;
                                        width: 80px" type="text" value='<%#Eval("unPrice") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <input id="WLNOTE" runat="server" style="border-style: none; width: 200px" type="text"
                                        value='<%#Eval("WLNOTE") %>' onkeyup="grControlFocus(this)" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle CssClass="bomcolor" ForeColor="#EEF7FD" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#A8B7EC" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div>
        <div id="win" visible="false">
            <div>
                <table>
                    <tr>
                        <td>
                            <strong>指定人员</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            按部门查询：
                        </td>
                        <td>
                            <input id="dep" name="dept" value="02" />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 430px; height: 230px">
                <table id="dg">
                </table>
            </div>
        </div>
        <div id="buttons" style="text-align: right" visible="false">
            <a class="easyui-linkbutton" data-options="iconCls:'icon-ok',plain:true" onclick="savePick();">
                保存</a> <a class="easyui-linkbutton" data-options="iconCls:'icon-cancel',plain:true"
                    onclick="javascript:$('#win').dialog('close')">取消</a>
            <input id="hidPerson" type="hidden" value="" />
        </div>
    </div>
</asp:Content>
