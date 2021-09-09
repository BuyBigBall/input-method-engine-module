<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.lblDebug = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.tray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.tray_menu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ts_about = New System.Windows.Forms.ToolStripMenuItem()
        Me.ts_configure = New System.Windows.Forms.ToolStripMenuItem()
        Me.ts_startup = New System.Windows.Forms.ToolStripMenuItem()
        Me.ts_exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.tray_menu.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDebug
        '
        Me.lblDebug.AutoSize = True
        Me.lblDebug.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDebug.Location = New System.Drawing.Point(28, 32)
        Me.lblDebug.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDebug.Name = "lblDebug"
        Me.lblDebug.Size = New System.Drawing.Size(149, 163)
        Me.lblDebug.TabIndex = 0
        Me.lblDebug.Text = "?"
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(18, 229)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(98, 57)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Status"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'tray
        '
        Me.tray.Icon = CType(resources.GetObject("tray.Icon"), System.Drawing.Icon)
        Me.tray.Text = "NotifyIcon1"
        Me.tray.Visible = True
        '
        'tray_menu
        '
        Me.tray_menu.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.tray_menu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_about, Me.ts_configure, Me.ts_startup, Me.ts_exit})
        Me.tray_menu.Name = "tray_menu"
        Me.tray_menu.Size = New System.Drawing.Size(202, 157)
        '
        'ts_about
        '
        Me.ts_about.Name = "ts_about"
        Me.ts_about.Size = New System.Drawing.Size(201, 30)
        Me.ts_about.Text = "About"
        '
        'ts_configure
        '
        Me.ts_configure.Name = "ts_configure"
        Me.ts_configure.Size = New System.Drawing.Size(201, 30)
        Me.ts_configure.Text = "Configure..."
        '
        'ts_startup
        '
        Me.ts_startup.Name = "ts_startup"
        Me.ts_startup.Size = New System.Drawing.Size(201, 30)
        Me.ts_startup.Text = "Add to startup"
        '
        'ts_exit
        '
        Me.ts_exit.Name = "ts_exit"
        Me.ts_exit.Size = New System.Drawing.Size(201, 30)
        Me.ts_exit.Text = "Exit"
        '
        'Button4
        '
        Me.Button4.Enabled = False
        Me.Button4.Location = New System.Drawing.Point(140, 235)
        Me.Button4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(136, 51)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Remove startup"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(94, 52)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.lblDebug)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmMain"
        Me.Text = "KeySwap"
        Me.WindowState = System.Windows.Forms.FormWindowState.Minimized
        Me.tray_menu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents lblDebug As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents tray As NotifyIcon
    Friend WithEvents tray_menu As ContextMenuStrip
    Friend WithEvents ts_exit As ToolStripMenuItem
    Friend WithEvents ts_about As ToolStripMenuItem
    Friend WithEvents ts_startup As ToolStripMenuItem
    Friend WithEvents Button4 As Button
    Friend WithEvents ts_configure As ToolStripMenuItem
End Class
